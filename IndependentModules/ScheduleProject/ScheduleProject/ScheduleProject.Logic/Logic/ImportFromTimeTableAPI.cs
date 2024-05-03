using Microsoft.Extensions.Logging;
using ScheduleProject.Logic.Infrastructure;
using ScheduleProject.Logic.Models.CoreModels;
using ScheduleProject.Logic.Models.ScheduleModels;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace ScheduleProject.Logic.Logic;

internal partial class ImportFromTimeTableAPI : IImportSchedule
{
	[GeneratedRegex(@"^(\w)+\.")]
	private partial Regex TypeLessonRegex();

	[GeneratedRegex(@"\-(\s)?\d(\s)?(п/г)$")]
	private partial Regex SubgroupRegex();

	private readonly char[] separator = ['.', ' '];

	private readonly List<DateTime> _scheduleLessonTimes =
			[
				DateTime.Now.Date.AddHours(8).AddMinutes(30),
				DateTime.Now.Date.AddHours(10).AddMinutes(00),
				DateTime.Now.Date.AddHours(11).AddMinutes(30),
				DateTime.Now.Date.AddHours(13).AddMinutes(30),
				DateTime.Now.Date.AddHours(15).AddMinutes(00),
				DateTime.Now.Date.AddHours(16).AddMinutes(30),
				DateTime.Now.Date.AddHours(18).AddMinutes(00),
				DateTime.Now.Date.AddHours(19).AddMinutes(30)
			];

	private ILogger<ImportFromTimeTableAPI> _logger;

	public ImportFromTimeTableAPI(ILogger<ImportFromTimeTableAPI> logger)
	{
		_logger = logger;
	}

	public async Task<IEnumerable<LessonScheduleModel>?> GetLessonsAsync(string baseUrl, 
		DateTime startDate,
		IEnumerable<Classroom> classrooms, 
		IEnumerable<Lecturer> lecturers, 
		IEnumerable<StudentGroup> studentGroups,
		CancellationToken cancellationToken)
	{
		var list = new List<LessonScheduleModel>();
		try
		{
			var client = CreateClient(baseUrl);
			// получаем список учебных групп, по которым пройдем и соберем все расписание
			var response = await client.GetAsync($"groups/", cancellationToken);
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
					try
					{
						// запрашиваем расписание
						response = await client.GetAsync($"/timetable/?filter={group}", cancellationToken);
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
										CalcLessonDateTime(lesson, week, day, couple);

										list.Add(lesson);
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						_logger?.LogError(ex, "Error while processing data from group {group}", group);
						return;
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
		var matchType = TypeLessonRegex().Match(lesson.Lecturer.Trim());
		if (matchType.Success)
		{
			lesson.LessonType = matchType.Value.ToLower();
			lesson.Discipline = lesson.Discipline.Remove(0, lesson.LessonType.Length).Trim();
		}
		var subgroupMatch = SubgroupRegex().Match(lesson.Discipline);
		if (subgroupMatch.Success)
		{
			lesson.Discipline = lesson.Discipline.Remove(lesson.Discipline.Length - subgroupMatch.Value.Length);
		}
	}

	private void CalcLessonDateTime(LessonScheduleModel lesson, WeekScheduleModel week, DayScheduleModel day, 
		CoupleScheduleModel couple)
	{
		lesson.Date = week.StartDate.Date
			.AddDays(day.DayOfWeek)
			.AddHours(_scheduleLessonTimes[couple.Number].Hour)
			.AddMinutes(_scheduleLessonTimes[couple.Number].Minute);
	}
}