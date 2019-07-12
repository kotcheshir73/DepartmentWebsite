using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace ScheduleInterfaces.BindingModels
{
    public class ScheduleLessonTimeGetBindingModel : PageSettingGetBinidingModel
    {
        public string Title { get; set; }
	}

	public class ScheduleLessonTimeSetBindingModel : PageSettingSetBinidingModel
    {
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