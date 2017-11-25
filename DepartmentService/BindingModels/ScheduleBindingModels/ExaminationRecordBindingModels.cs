using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class ExaminationRecordGetBindingModel
	{
		public long Id { get; set; }
	}

	public class ExaminationRecordRecordBindingModel
	{
		public long Id { get; set; }

		[Required(ErrorMessage = "required")]
		public DateTime DateConsultation { get; set; }

		[Required(ErrorMessage = "required")]
		public DateTime DateExamination { get; set; }

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
