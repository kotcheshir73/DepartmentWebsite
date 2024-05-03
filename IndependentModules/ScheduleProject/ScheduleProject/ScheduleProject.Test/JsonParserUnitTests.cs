using ScheduleProject.Logic.Infrastructure;

namespace ScheduleProject.Test;

public class Tests
{
	private readonly string _filePath = "//Resources//ScheduleResponseTestData.json";

	[SetUp]
	public void Setup()
	{
	}

	[Test]
	public void Success_Test()
	{
		var date = DateTime.UtcNow.Date;
		var result = JsonParser.ParseScheduleData(GetDataFromFile(), date);

		Assert.That(result, Is.Not.Null);
		Assert.That(result.Count(), Is.EqualTo(2));

		var week = result.First();
		Assert.Multiple(() =>
		{
			Assert.That(week.StartDate, Is.EqualTo(date));
			Assert.That(week.Days.Count(), Is.EqualTo(6));
		});

		var secondWeek = result.Last();
		Assert.Multiple(() =>
		{
			Assert.That(secondWeek.StartDate, Is.EqualTo(date.AddDays(7 * 3)));
			Assert.That(secondWeek.Days.Count(), Is.EqualTo(6));
		});

		var firstDay = week.Days.First();
		Assert.Multiple(() =>
		{
			Assert.That(firstDay.DayOfWeek, Is.EqualTo(0));
			Assert.That(firstDay.Couples.Count(), Is.EqualTo(8));
		});

		var couples = firstDay.Couples.ToList();
		Assert.Multiple(() =>
		{
			Assert.That(couples[0].Number, Is.EqualTo(0));
			Assert.That(couples[0].Lessons.Count(), Is.EqualTo(0));
			Assert.That(couples[1].Number, Is.EqualTo(1));
			Assert.That(couples[1].Lessons.Count(), Is.EqualTo(3));
			Assert.That(couples[2].Number, Is.EqualTo(2));
			Assert.That(couples[2].Lessons.Count(), Is.EqualTo(2));
		});

		var lessons = couples[1].Lessons.ToList();
		Assert.Multiple(() =>
		{
			Assert.That(lessons[0].Discipline, Is.EqualTo("Занятие 1"));
			Assert.That(lessons[1].Discipline, Is.EqualTo("Занятие 1"));
			Assert.That(lessons[2].Discipline, Is.EqualTo("Занятие 1"));
			Assert.That(lessons[0].Lecturer, Is.EqualTo("Преподаватель 1"));
			Assert.That(lessons[1].Lecturer, Is.EqualTo("Преподаватель 1"));
			Assert.That(lessons[2].Lecturer, Is.EqualTo("Преподаватель 1"));
			Assert.That(lessons[0].Classroom, Is.EqualTo("Аудитория 1"));
			Assert.That(lessons[1].Classroom, Is.EqualTo("Аудитория 1"));
			Assert.That(lessons[2].Classroom, Is.EqualTo("Аудитория 1"));
			Assert.That(lessons[0].Group, Is.EqualTo("Группа 1"));
			Assert.That(lessons[1].Group, Is.EqualTo("Группа 2"));
			Assert.That(lessons[2].Group, Is.EqualTo("Группа 3"));
		});

		lessons = couples[2].Lessons.ToList();
		Assert.Multiple(() =>
		{
			Assert.That(lessons[0].Discipline, Is.EqualTo("Занятие 2"));
			Assert.That(lessons[1].Discipline, Is.EqualTo("Занятие 3"));
			Assert.That(lessons[0].Lecturer, Is.EqualTo("Преподаватель 1"));
			Assert.That(lessons[1].Lecturer, Is.EqualTo("Преподаватель 2"));
			Assert.That(lessons[0].Classroom, Is.EqualTo("Аудитория 2"));
			Assert.That(lessons[1].Classroom, Is.EqualTo("Аудитория 3"));
			Assert.That(lessons[0].Group, Is.EqualTo("Группа 1"));
			Assert.That(lessons[1].Group, Is.EqualTo("Группа 1"));
		});
	}

	private string GetDataFromFile()
	{
		var filePath = $"{Directory.GetCurrentDirectory()}{_filePath}";
		return File.Exists(filePath) ? File.ReadAllText(filePath) : string.Empty;
	}
}