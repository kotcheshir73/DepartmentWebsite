namespace ScheduleSyncProject.Logic.Models.ScheduleModels;

/// <summary>
/// Учебная неделя
/// </summary>
internal class WeekScheduleModel
{
	public DateTime StartDate { get; set; }

	public required IEnumerable<DayScheduleModel> Days { get; set; }
}