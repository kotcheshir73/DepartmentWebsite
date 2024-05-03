namespace ScheduleProject.Logic.Models.ScheduleModels;

public class LessonScheduleModel
{
	public required string Group { get; set; }

	public required string Discipline { get; set; }

	public required string Lecturer { get; set; }

	public required string Classroom { get; set; }

	public Guid? ClassroomId { get; set; }

	public Guid? LecturerId { get; set; }

	public Guid? StudentGroupId { get; set; }

	public string? LessonType { get; set; }

	public DateTime Date { get; set; }
}