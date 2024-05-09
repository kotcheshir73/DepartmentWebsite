namespace ScheduleSyncProject.Logic.Models.ScheduleModels;

/// <summary>
/// Учебная пара (например, 1 неделя, понедлеьник, 3 пара, Lessons - список пар в это время)
/// </summary>
internal class CoupleScheduleModel
{
	public int Number { get; set; }

	public required IEnumerable<LessonScheduleModel> Lessons { get; set; }
}