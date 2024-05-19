using Azure;
using Microsoft.Extensions.Logging;
using ScheduleSyncProject.Logic.Infrastructure;
using ScheduleSyncProject.Logic.Logic.Repository;
using ScheduleSyncProject.Logic.Models.CoreModels;
using ScheduleSyncProject.Logic.Models.ScheduleModels;
using System.Collections.Generic;
using System.Net.Http;

namespace ScheduleSyncProject.Logic.Logic;

public class SynchronizationScheduleLogic(IImportSchedule importSchedule, ICoreRepository coreRepository,
	ILogger<SynchronizationScheduleLogic> logger)
{
	private readonly IImportSchedule _importSchedule =
		importSchedule ?? throw new ArgumentNullException(nameof(importSchedule));

	private readonly ICoreRepository _coreRepository =
		coreRepository ?? throw new ArgumentNullException(nameof(coreRepository));

	private readonly ILogger<SynchronizationScheduleLogic> _logger = logger;

	public async Task<bool> SyncScheduleAsync(Action<string> notification, CancellationToken cancellationToken)
	{
		try
		{
			var startSemesterDate = await GetCurrentStartSemesterDateAsync(cancellationToken);
			_logger.LogInformation("Start sync schedule for date {date}", startSemesterDate.ToShortDateString());

			var groups = await _importSchedule.GetGroupsAsync(cancellationToken);
			if (groups is null)
			{
				_logger.LogWarning("No groups");
				return false;
			}

			var classrooms = await _coreRepository.GetClassroomsAsync(cancellationToken) ?? throw new InvalidOperationException("Not found classrooms");
			var lecturers = await _coreRepository.GetLecturersAsync(cancellationToken) ?? throw new InvalidOperationException("Not found lecturers");
			var studentgroups = await _coreRepository.GetStudentGroupsAsync(cancellationToken) ?? throw new InvalidOperationException("Not found groups");
			var records = await _coreRepository.GetSemesterRecordsAsync(DateTime.UtcNow.Date.AddDays(-14), DateTime.UtcNow.Date.AddDays(14), cancellationToken) ?? throw new InvalidOperationException("Not found semester records");

			var tasks = new List<Task>();
			var beginDate = DateTime.UtcNow;
			var endDate = DateTime.UtcNow;
			var resetEvent = new AutoResetEvent(true);
			var semaphore = new SemaphoreSlim(5, 5);

			notification($"Need processing {groups.Count()} groups...");
			foreach (var group in groups)
			{
				tasks.Add(Task.Run(async () =>
				{
					while (true)
					{
						try
						{
							semaphore.Wait();
							notification($"Start processing group {group}");
							var lessons = await _importSchedule.GetLessonsAsync(group, startSemesterDate, classrooms, lecturers, studentgroups,
				cancellationToken);
							if (lessons is null || !lessons.Any())
							{
								return;
							}

							lessons = lessons.OrderBy(x => x.Date);
							resetEvent.WaitOne();
							{
								if (beginDate > lessons.First().Date)
								{
									beginDate = lessons.First().Date;
								}

								if (endDate < lessons.Last().Date)
								{
									endDate = lessons.Last().Date;
								}
								resetEvent.Set();
							}

							var newRecords = lessons.Where(x => CheckAndSaveLesson(x, records)).Select(x => CreateSemesterRecord(x));
							if (newRecords.Any())
							{
								var tasks = new List<Task>();
								foreach(var record in newRecords)
								{
									tasks.Add(_coreRepository.SaveSemesterRecordAsync(record, cancellationToken));
								}
								await Task.WhenAll(tasks);
								_logger.LogInformation("{count} records saving", newRecords.Count());
							}
							notification($"Finish processing group {group}");
							return;
						}
						catch (Exception ex)
						{
							_logger?.LogError(ex, "Error while processing data from group {group}", group);
							return;
						}
						finally
						{
							semaphore.Release();
						}
					}
				}, cancellationToken));
			}

			await Task.WhenAll(tasks);

			tasks.Clear();

			foreach(var id in records.Where(x => x.ScheduleDate >= beginDate && x.ScheduleDate <= endDate && !x.Found).Select(x => x.Id))
			{
				tasks.Add(_coreRepository.RemoveSemesterRecordAsync(id,cancellationToken));
			}
			await Task.WhenAll(tasks);

			_logger.LogInformation("Finish sync schedule for date {date}", startSemesterDate);

			return true;
		}
		catch (AggregateException ex)
		{
			foreach (var excep in ex.InnerExceptions)
			{
				_logger.LogError(excep, "Error while sync lessons");
			}

			return false;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error while sync lessons");
			return false;
		}
	}

	private async Task<DateTime> GetCurrentStartSemesterDateAsync(CancellationToken cancellationToken)
	{
		var dates = await _coreRepository.GetSeasonDatesAsync(cancellationToken);
		if (dates == null || !dates.Any())
		{
			throw new InvalidOperationException("Failed to extract semester start dates");
		}

		var currentDate = DateTime.UtcNow;
		var selectedDates = dates.Where(x => x.DateBeginFirstHalfSemester.Year == currentDate.Year);
		if (!selectedDates.Any())
		{
			throw new InvalidOperationException($"No dates for {currentDate.Year} year");
		}

		return currentDate.Month > 7 ?
			selectedDates.Single(x => x.DateBeginFirstHalfSemester.Month > 6).DateBeginFirstHalfSemester :
			selectedDates.Single(x => x.DateBeginFirstHalfSemester.Month < 6).DateBeginFirstHalfSemester;
	}

	private static bool CheckAndSaveLesson(LessonScheduleModel lesson, IEnumerable<SemesterRecord> records)
	{
		var exsistRecord = records.FirstOrDefault(x =>
			x.ScheduleDate == lesson.Date &&
			x.LessonClassroom == lesson.Classroom &&
			x.LessonLecturer == lesson.Lecturer &&
			x.LessonDiscipline == lesson.Discipline &&
			x.LessonStudentGroup == lesson.Group);

		if (exsistRecord != null)
		{
			exsistRecord.Found = true;
			return false;
		}

		return true;
	}

	private static SemesterRecord CreateSemesterRecord(LessonScheduleModel lesson)
	{
		return new SemesterRecord
		{
			Id = Guid.NewGuid(),
			ScheduleDate = lesson.Date,
			LessonType = lesson.LessonType,
			ClassroomId = lesson.ClassroomId,
			LecturerId = lesson.LecturerId,
			StudentGroupId = lesson.StudentGroupId,
			LessonClassroom = lesson.Classroom,
			LessonDiscipline = lesson.Discipline,
			LessonLecturer = lesson.Lecturer,
			LessonStudentGroup = lesson.Group
		};
	}
}