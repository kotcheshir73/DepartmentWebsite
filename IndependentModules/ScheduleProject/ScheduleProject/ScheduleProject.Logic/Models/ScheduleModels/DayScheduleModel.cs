namespace ScheduleProject.Logic.Models.ScheduleModels;

internal class DayScheduleModel
{
	public int DayOfWeek { get; set; }

	public required IEnumerable<CoupleScheduleModel> Couples { get; set; }
}