using DepartmentService.BindingModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace ScheduleServiceInterfaces.BindingModels
{
	public class ScheduleLessonTimeGetBindingModel : PageSettingBinidingModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
	}

	public class ScheduleLessonTimeSetBindingModel
	{
		public Guid Id { get; set; }

        [Required(ErrorMessage = "required")]
		public string Title { get; set; }

        [Required(ErrorMessage = "required")]
        public int Order { get; set; }

        [Required(ErrorMessage = "required")]
		public DateTime DateBeginLesson { get; set; }

		[Required(ErrorMessage = "required")]
		public DateTime DateEndLesson { get; set; }
	}
}