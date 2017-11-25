using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class ConsultationRecordGetBindingModel
	{
		public long Id { get; set; }

		public DateTime DateBegin { get; set; }

		public DateTime DateEnd { get; set; }
	}

	public class ConsultationRecordRecordBindingModel
	{
		public long Id { get; set; }

		public int? Week { get; set; }

		public int? Day { get; set; }

		public int? Lesson { get; set; }

		[Required(ErrorMessage = "required")]
		public DateTime DateConsultation { get; set; }

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
