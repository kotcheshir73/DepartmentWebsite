using System.ComponentModel.DataAnnotations;

namespace ScheduleInterfaces.BindingModels
{
	public class SemesterRecordRecordBindingModel : ScheduleSetBindingModel
    {
        public bool IsFirstHalfSemester { get; set; }

        [Required(ErrorMessage = "required")]
        public string LessonType { get; set; }

        public int Week { get; set; }

		public int Day { get; set; }

		public int Lesson { get; set; }

		public bool IsStreaming { get; set; }

        public bool IsSubgroup { get; set; }
    }
}