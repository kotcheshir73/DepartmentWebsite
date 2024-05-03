using System.ComponentModel.DataAnnotations;

namespace ScheduleProjectApp.Logic.Models;

internal class SemesterRecordSetBindingModel : ScheduleSetBindingModel
{
	[Required(ErrorMessage = "required")]
	public LessonTypes LessonType { get; set; }

	public int Period { get; set; }

	public int Week { get; set; }

	public int Day { get; set; }

	public int Lesson { get; set; }
}
