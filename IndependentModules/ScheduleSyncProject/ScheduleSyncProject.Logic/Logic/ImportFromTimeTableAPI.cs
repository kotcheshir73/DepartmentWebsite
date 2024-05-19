using Azure;
using Microsoft.Extensions.Logging;
using ScheduleSyncProject.Logic.Infrastructure;
using ScheduleSyncProject.Logic.Models.CoreModels;
using ScheduleSyncProject.Logic.Models.ScheduleModels;
using ScheduleSyncProject.Logic.Models.SettingModels;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace ScheduleSyncProject.Logic.Logic;

internal partial class ImportFromTimeTableAPI(TimeTableAPISettings timeTableAPISettings, ILogger<ImportFromTimeTableAPI> logger) : IImportSchedule
{
	[GeneratedRegex(@"^(\w)+\.")]
	private partial Regex TypeLessonRegex();

	[GeneratedRegex(@"\-(\s)?\d(\s)?(п/г)$")]
	private partial Regex SubgroupRegex();

	private readonly char[] separator = ['.', ' '];

	private readonly ILogger<ImportFromTimeTableAPI> _logger = logger;

	private readonly HttpClient _httpClient = CreateClient(timeTableAPISettings.BaseUrl);

	public async Task<IEnumerable<LessonScheduleModel>?> GetLessonsAsync(DateTime startDate,
		IEnumerable<Classroom> classrooms,
		IEnumerable<Lecturer> lecturers,
		IEnumerable<StudentGroup> studentGroups,
		CancellationToken cancellationToken)
	{
		var list = new List<LessonScheduleModel>();
		try
		{
			// получаем список учебных групп, по которым пройдем и соберем все расписание
			var response = await _httpClient.GetAsync($"groups/", cancellationToken);
			if (!response.IsSuccessStatusCode)
			{
				_logger?.LogError("Error on loading data groups. Code: {code}", response.StatusCode);
				return null;
			}

			var dataOfResposne = await response.Content.ReadAsStringAsync(cancellationToken);
			var groups = JsonParser.ParseGroupData(dataOfResposne);
			if (groups == null)
			{
				_logger?.LogError("Error on parsing response by groups. {data}", dataOfResposne);
				return null;
			}

			var tasks = new List<Task>();
			// для каждой группы запускаем паралелльно обработку его расписания
			foreach (var group in groups.Where(x => !string.IsNullOrEmpty(x)))
			{
				tasks.Add(Task.Run(async () =>
				{
					while (true)
					{
						try
						{
							// запрашиваем расписание
							response = await _httpClient.GetAsync($"timetable/?filter={group}", cancellationToken);
							if (!response.IsSuccessStatusCode)
							{
								_logger?.LogError("Error on loading data group {group}. Code: {code}", group, response.StatusCode);
								return;
							}

							var dataOfResposne = await response.Content.ReadAsStringAsync(cancellationToken);
							var schedule = JsonParser.ParseScheduleData(dataOfResposne, startDate);
							if (schedule is null)
							{
								_logger?.LogError("Error on parsing response by group {group}. {data}", group, dataOfResposne);
								return;
							}

							foreach (var week in schedule)
							{
								foreach (var day in week.Days)
								{
									foreach (var couple in day.Couples.Where(x => x.Lessons.Any()))
									{
										foreach (var lesson in couple.Lessons)
										{
											lesson.ClassroomId = classrooms.FirstOrDefault(x => x.Number == lesson.Classroom)?.Id;
											lesson.LecturerId = lecturers.FirstOrDefault(x => x.ToString() == GetLecturerName(lesson))?.Id;
											lesson.StudentGroupId = studentGroups.FirstOrDefault(x => x.GroupName == lesson.Group)?.Id;

											if (!lesson.ClassroomId.HasValue && !lesson.LecturerId.HasValue && !lesson.StudentGroupId.HasValue)
											{
												continue;
											}

											PrepareDiscipline(lesson);
											lesson.Date = CalcLessonDateTime(week, day, couple);

											list.Add(lesson);
										}
									}
								}
							}
							return;
						}
						catch (TimeoutException)
						{
							await Task.Delay(TimeSpan.FromMilliseconds(50));
						}
						catch (Exception ex)
						{
							_logger?.LogError(ex, "Error while processing data from group {group}", group);
							return;
						}
					}
				}, cancellationToken));

			}

			await Task.WhenAll(tasks);
		}
		catch
		{
			return null;
		}

		return list.Distinct(new LessonEqualityComparer());
	}

	public async Task<IEnumerable<string>?> GetGroupsAsync(CancellationToken cancellationToken)
	{
		try
		{
			var response = await _httpClient.GetAsync($"groups/", cancellationToken);
			if (!response.IsSuccessStatusCode)
			{
				_logger?.LogError("Error on loading data groups. Code: {code}", response.StatusCode);
				return null;
			}

			var dataOfResposne = await response.Content.ReadAsStringAsync(cancellationToken);
			return JsonParser.ParseGroupData(dataOfResposne);
		}
		catch (Exception ex)
		{
			_logger?.LogError(ex, "Error while processing groups");
			return null;
		}
	}

	public async Task<IEnumerable<LessonScheduleModel>?> GetLessonsAsync(string groupName, DateTime startDate, IEnumerable<Classroom> classrooms, IEnumerable<Lecturer> lecturers, IEnumerable<StudentGroup> studentGroups, CancellationToken cancellationToken)
	{
		while (true)
		{
			try
			{
				// запрашиваем расписание
				var response = await _httpClient.GetAsync($"timetable/?filter={groupName}", cancellationToken);
				if (!response.IsSuccessStatusCode)
				{
					_logger?.LogError("Error on loading data group {group}. Code: {code}", groupName, response.StatusCode);
					return null;
				}

				var dataOfResposne = await response.Content.ReadAsStringAsync(cancellationToken);
				var schedule = JsonParser.ParseScheduleData(dataOfResposne, startDate);
				if (schedule is null)
				{
					_logger?.LogError("Error on parsing response by group {group}. {data}", groupName, dataOfResposne);
					return null;
				}

				var list = new List<LessonScheduleModel>();
				foreach (var week in schedule)
				{
					foreach (var day in week.Days)
					{
						foreach (var couple in day.Couples.Where(x => x.Lessons.Any()))
						{
							foreach (var lesson in couple.Lessons)
							{
								lesson.ClassroomId = classrooms.FirstOrDefault(x => x.Number == lesson.Classroom)?.Id;
								lesson.LecturerId = lecturers.FirstOrDefault(x => x.ToString() == GetLecturerName(lesson))?.Id;
								lesson.StudentGroupId = studentGroups.FirstOrDefault(x => x.GroupName == lesson.Group)?.Id;

								if (!lesson.ClassroomId.HasValue && !lesson.LecturerId.HasValue && !lesson.StudentGroupId.HasValue)
								{
									continue;
								}

								PrepareDiscipline(lesson);
								lesson.Date = CalcLessonDateTime(week, day, couple);

								list.Add(lesson);
							}
						}
					}
				}

				return list;
			}
			catch (TimeoutException)
			{
				await Task.Delay(TimeSpan.FromSeconds(500), cancellationToken);
			}
			catch (Exception ex)
			{
				_logger?.LogError(ex, "Error while processing data from group {group}", groupName);
				return null;
			}
		}
	}

	private static HttpClient CreateClient(string baseUrl)
	{
		var client = new HttpClient
		{
			BaseAddress = new Uri(baseUrl)
		};
		client.DefaultRequestHeaders.Accept.Clear();
		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		return client;
	}

	private string GetLecturerName(LessonScheduleModel lesson)
	{
		if (string.IsNullOrEmpty(lesson.Lecturer))
		{
			return string.Empty;
		}

		var spliters = lesson.Lecturer.Split(separator, StringSplitOptions.RemoveEmptyEntries);
		var lastName = $"{spliters[0][0].ToString().ToUpper()}{spliters[0][1..].ToLower()}";
		var firstName = spliters.Length > 1 ? $" {spliters[1][0].ToString().ToUpper()}." : string.Empty;
		var patronumic = spliters.Length > 2 ? $" {spliters[2][0].ToString().ToUpper()}." : string.Empty;
		return $"{lastName}{firstName}{patronumic}";
	}

	private void PrepareDiscipline(LessonScheduleModel lesson)
	{
		// оперделяем тип занятия
		var matchType = TypeLessonRegex().Match(lesson.Discipline.Trim());
		if (matchType.Success)
		{
			lesson.LessonType = GetLessonType(matchType.Value.ToLower());
			lesson.Discipline = lesson.Discipline.Remove(0, matchType.Value.Length).Trim();
		}
		var subgroupMatch = SubgroupRegex().Match(lesson.Discipline);
		if (subgroupMatch.Success)
		{
			lesson.Discipline = lesson.Discipline.Remove(lesson.Discipline.Length - subgroupMatch.Value.Length);
		}
	}

	private static DateTime CalcLessonDateTime(WeekScheduleModel week, DayScheduleModel day, CoupleScheduleModel couple) =>
			week.StartDate.Date.AddDays(day.DayOfWeek).AddTicks(ScheduleLessonTimes.GetTicks(couple.Number));

	private static LessonTypes GetLessonType(string value) => value.ToLower() switch
	{
		"лек." => LessonTypes.лек,
		"пр." => LessonTypes.пр,
		"лаб." => LessonTypes.лаб,
		"зач." => LessonTypes.зачет,
		"экз." => LessonTypes.экзамен,
		_ => LessonTypes.нд,
	};
}