using System;

namespace ScheduleInterfaces.ViewModels
{
	/// <summary>
	/// Краткая запись для вывода в расписании
	/// </summary>
	public class OffsetRecordShortViewModel : ScheduleRecordShortViewModel
    {
		public int Lesson { get; set; }

		public string Text
		{
			get
			{
				return string.Format("{0} {1}{2}{3}{2}{4}", LessonDiscipline, LessonClassroom, Environment.NewLine, LessonLecturer, LessonGroup);
			}
		}
	}

	/// <summary>
	/// Полная запись для редактирования
	/// </summary>
	public class OffsetRecordViewModel : ScheduleRecordViewModel
    {
		public int Lesson { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}{3}{4}{3}{5}", LessonType, LessonDiscipline, LessonClassroom, Environment.NewLine, LessonLecturer, LessonStudentGroup);
        }
    }
}