using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class OffsetRecordGetBindingModel
	{
		public long Id { get; set; }
	}

	public class OffsetRecordRecordBindingModel
	{
		public long Id { get; set; }

		public int Week { get; set; }

		public int Day { get; set; }

		public int Lesson { get; set; }

		public string NotParseRecord { get; set; }

		[Required(ErrorMessage = "required")]
		public string LessonDiscipline { get; set; }

		[Required(ErrorMessage = "required")]
		public string LessonLecturer { get; set; }

		[Required(ErrorMessage = "required")]
		public string LessonGroup { get; set; }

		[Required(ErrorMessage = "required")]
		public string LessonClassroom { get; set; }

		public long? LecturerId { get; set; }

		public long? StudentGroupId { get; set; }

		public string ClassroomId { get; set; }
	}
}
