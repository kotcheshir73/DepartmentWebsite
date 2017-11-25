using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class SemesterRecordGetBindingModel
	{
		public long Id { get; set; }
	}

	public class SemesterRecordRecordBindingModel
	{
		public long Id { get; set; }

		public long? SeasonDatesId { get; set; }

		public int Week { get; set; }

		public int Day { get; set; }

		public int Lesson { get; set; }

		public string NotParseRecord { get; set; }

		public bool IsStreaming { get; set; }

		[Required(ErrorMessage = "required")]
		public string LessonType { get; set; }

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
