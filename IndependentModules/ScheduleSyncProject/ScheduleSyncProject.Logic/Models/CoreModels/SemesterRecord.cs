using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleSyncProject.Logic.Models.CoreModels;

public class SemesterRecord
{
	public Guid Id { get; set; } = Guid.NewGuid();

	public Guid? ClassroomId { get; set; }

	public Guid? LecturerId { get; set; }

	public Guid? StudentGroupId { get; set; }

	public LessonTypes LessonType { get; set; }

	public DateTime ScheduleDate { get; set; }

	public required string LessonClassroom { get; set; }

	public required string LessonDiscipline { get; set; }

	public required string LessonLecturer { get; set; }

	public required string LessonStudentGroup { get; set; }

	[NotMapped]
	public bool Found { get; set; }
}