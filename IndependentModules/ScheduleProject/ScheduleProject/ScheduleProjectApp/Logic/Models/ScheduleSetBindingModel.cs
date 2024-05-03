using System.ComponentModel.DataAnnotations;

namespace ScheduleProjectApp.Logic.Models;

internal class ScheduleSetBindingModel
{
	public Guid Id;

	public string NotParseRecord { get; set; }

	[Required(ErrorMessage = "required")]
	public DateTime ScheduleDate { get; set; }

	[Required(ErrorMessage = "required")]
	public string LessonClassroom { get; set; }

	[Required(ErrorMessage = "required")]
	public string LessonDiscipline { get; set; }

	[Required(ErrorMessage = "required")]
	public string LessonLecturer { get; set; }

	[Required(ErrorMessage = "required")]
	public string LessonStudentGroup { get; set; }

	public Guid? ClassroomId { get; set; }

	public Guid? DisciplineId { get; set; }

	public Guid? LecturerId { get; set; }

	public Guid? StudentGroupId { get; set; }
}
