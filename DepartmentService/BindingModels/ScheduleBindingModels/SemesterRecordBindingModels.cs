using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class SemesterRecordRecordBindingModel : ScheduleRecordBindingModel
    {
        // TODO проверить надобность, возможно только для ScheduleService
        public long? SeasonDatesId { get; set; }

        [Required(ErrorMessage = "required")]
        public string LessonType { get; set; }

        public int Week { get; set; }

		public int Day { get; set; }

		public int Lesson { get; set; }

		public bool IsStreaming { get; set; }

		/// <summary>
		/// Применять выборку по текстовым данным или по данным из БД (аудитория, дисциплина, преподаватель, группа)
		/// </summary>
		public bool ApplyToAnalogRecordsByTextData { get; set; }

		public bool ApplyToAnalogRecordsByDiscipline { get; set; }

		public bool ApplyToAnalogRecordsByGroup { get; set; }

		public bool ApplyToAnalogRecordsByLecturer { get; set; }

		public bool ApplyToAnalogRecordsByClassroom { get; set; }

		public bool ApplyToAnalogRecordsByLessonType { get; set; }
	}
}
