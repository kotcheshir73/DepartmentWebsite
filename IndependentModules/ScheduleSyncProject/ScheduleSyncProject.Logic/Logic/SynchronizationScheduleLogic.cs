using Microsoft.Extensions.Logging;
using ScheduleSyncProject.Logic.Logic.Repository;
using ScheduleSyncProject.Logic.Models.CoreModels;
using ScheduleSyncProject.Logic.Models.ScheduleModels;

namespace ScheduleSyncProject.Logic.Logic;

public class SynchronizationScheduleLogic(IImportSchedule importSchedule, ICoreRepository coreRepository,
	ILogger<SynchronizationScheduleLogic> logger)
{
	private readonly IImportSchedule _importSchedule =
		importSchedule ?? throw new ArgumentNullException(nameof(importSchedule));

	private readonly ICoreRepository _coreRepository =
		coreRepository ?? throw new ArgumentNullException(nameof(coreRepository));

	private readonly ILogger<SynchronizationScheduleLogic> _logger = logger;

	public async Task<bool> SyncScheduleAsync(DateTime dateStart, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Start sync schedule for date {date}", dateStart);

		try
		{
			var classrooms = await _coreRepository.GetClassroomsAsync(cancellationToken) ?? throw new InvalidOperationException("Not found classrooms");
			var lecturers = await _coreRepository.GetLecturersAsync(cancellationToken) ?? throw new InvalidOperationException("Not found lecturers");
			var groups = await _coreRepository.GetStudentGroupsAsync(cancellationToken) ?? throw new InvalidOperationException("Not found groups");

			var lessons = await _importSchedule.GetLessonsAsync(dateStart, classrooms, lecturers, groups,
				cancellationToken) ?? throw new InvalidOperationException("No lessons for save");

			if (!lessons.Any())
			{
				_logger.LogWarning("Not found records");
				return false;
			}

			_logger.LogInformation("Found {count} records", lessons.Count());

			lessons = lessons.OrderBy(x => x.Date);

			var records = await _coreRepository.GetSemesterRecordsAsync(lessons.First().Date, lessons.Last().Date, cancellationToken) ?? throw new InvalidOperationException("Not found semester records");

			var tasks = lessons.Select(x => CheckAndSaveLessonAsync(x, records, cancellationToken));
			await Task.WhenAll(tasks);

			_logger.LogInformation("Records saving");

			await _coreRepository.RemoveSemesterRecordsAsync(records.Where(x => !x.Found).Select(x => x.Id), cancellationToken);

			_logger.LogInformation("Finish sync schedule for date {date}", dateStart);

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

	private Task CheckAndSaveLessonAsync(LessonScheduleModel lesson, IEnumerable<SemesterRecord> records, CancellationToken cancellationToken)
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
			return Task.CompletedTask;
		}

		return _coreRepository.SaveSemesterRecordAsync(CreateSemesterRecord(lesson), cancellationToken);
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