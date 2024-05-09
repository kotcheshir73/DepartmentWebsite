namespace ScheduleSyncProject.Logic.Models.ScheduleModels;

/// <summary>
/// Учебный день
/// </summary>
internal class DayScheduleModel
{
	public int DayOfWeek { get; set; }

	public required IEnumerable<CoupleScheduleModel> Couples { get; set; }
}