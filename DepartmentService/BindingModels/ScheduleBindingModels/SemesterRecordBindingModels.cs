using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class SemesterRecordRecordBindingModel : ScheduleRecordBindingModel
    {
        [Required(ErrorMessage = "required")]
        public string LessonType { get; set; }

        public int Week { get; set; }

		public int Day { get; set; }

		public int Lesson { get; set; }

		public bool IsStreaming { get; set; }
	}
}
