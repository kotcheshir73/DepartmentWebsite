namespace ScheduleProject.Logic.Models.ScheduleModels;

internal class CoupleScheduleModel
{
	public int Number { get; set; }

	public required IEnumerable<LessonScheduleModel> Lessons { get; set; }
}