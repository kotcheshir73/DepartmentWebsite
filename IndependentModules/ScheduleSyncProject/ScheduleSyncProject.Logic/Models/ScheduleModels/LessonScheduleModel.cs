using ScheduleSyncProject.Logic.Models.CoreModels;

namespace ScheduleSyncProject.Logic.Models.ScheduleModels;

/// <summary>
/// Занятие
/// </summary>
public class LessonScheduleModel
{
	public required string Group { get; set; }

	public required string Discipline { get; set; }

	public required string Lecturer { get; set; }

	public required string Classroom { get; set; }

	public Guid? ClassroomId { get; set; }

	public Guid? LecturerId { get; set; }

	public Guid? StudentGroupId { get; set; }

	public LessonTypes LessonType { get; set; }

	public DateTime Date { get; set; }
}