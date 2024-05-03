using Newtonsoft.Json.Linq;
using ScheduleProject.Logic.Models.ScheduleModels;
using ScheduleProject.Logic.Models.ScheduleModels.TimeTableAPI;

namespace ScheduleProject.Logic.Infrastructure;

internal class JsonParser
{
	public static IEnumerable<string>? ParseGroupData(string jsonText)
	{
		if (string.IsNullOrEmpty(jsonText))
		{
			return null;
		}

		return JObject.Parse(jsonText)?["response"]?.ToObject<string[]>();
	}

	public static IEnumerable<WeekScheduleModel>? ParseScheduleData(string jsonText, DateTime startDate)
	{
		if (string.IsNullOrEmpty(jsonText))
		{
			return null;
		}

		var weeks = JObject.Parse(jsonText)?["response"]?["weeks"];
		if (weeks is null)
		{
			return null;
		}

		var list = new List<WeekScheduleModel>();
		foreach (var week in weeks)
		{
			var name = week.GetType().GetProperties().FirstOrDefault(x => x.Name == "Name")?.GetValue(week)?.ToString();
			if (!int.TryParse(name, out var weekNumber))
			{
				continue;
			}
			var currentDate = startDate.AddDays(weekNumber * 7);

			var days = week?.First?.ToObject<TimeTableAPIScheduleWeek>();
			if (days is null)
			{
				continue;
			}

			list.Add(new WeekScheduleModel
			{
				StartDate = currentDate,
				Days = days.Days.Select(x => new DayScheduleModel
				{ 
					DayOfWeek = x.Day,
					Couples = x.Lessons.Select((y, i) => new CoupleScheduleModel
					{ 
						Number = i,
						Lessons = y.Select(z => new LessonScheduleModel
						{
							 Classroom = z.Room,
							 Discipline = z.NameOfLesson,
							 Group = z.Group,
							 Lecturer = z.Teacher
						})
					})
				})
			});
		}

		return list;
	}
}