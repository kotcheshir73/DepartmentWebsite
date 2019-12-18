using System;

namespace ScheduleInterfaces.ViewModels
{
    /// <summary>
    /// Краткая запись для вывода в расписании
    /// </summary>
    public class SemesterRecordShortViewModel : ScheduleRecordShortViewModel
    {
		public int Week { get; set; }

		public int Day { get; set; }

		public int Lesson { get; set; }

        public bool IsFirstSemester { get; set; }

        public bool IsFirstHalfSemester { get; set; }

        public string Text
		{
			get
			{
				return string.Format("{0} {1}{2}{3}{2}{4}", LessonType, LessonDiscipline, Environment.NewLine, LessonLecturer, LessonGroup);
			}
		}
	}

	/// <summary>
	/// Полная запись для редактирования
	/// </summary>
	public class SemesterRecordViewModel : ScheduleRecordViewModel
    {
		public int Week { get; set; }

		public int Day { get; set; }

		public int Lesson { get; set; }

        public bool IsFirstSemester { get; set; }

        public bool IsFirstHalfSemester { get; set; }
    }
}