using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class ScheduleLessonTimeGetBindingModel
	{
		public long Id { get; set; }

		public string Title { get; set; }
	}

	public class ScheduleLessonTimeRecordBindingModel
	{
		public long Id { get; set; }

		[Required(ErrorMessage = "required")]
		public string Title { get; set; }

		[Required(ErrorMessage = "required")]
		public DateTime DateBeginLesson { get; set; }

		[Required(ErrorMessage = "required")]
		public DateTime DateEndLesson { get; set; }
	}
}
