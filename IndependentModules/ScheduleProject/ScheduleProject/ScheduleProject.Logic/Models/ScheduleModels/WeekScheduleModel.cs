namespace ScheduleProject.Logic.Models.ScheduleModels;

internal class WeekScheduleModel
{
	public DateTime StartDate { get; set; }

	public required IEnumerable<DayScheduleModel> Days { get; set; }
}