using System;

namespace ScheduleInterfaces.ViewModels
{
	/// <summary>
	/// Краткая запись для вывода в расписании
	/// </summary>
	public class ConsultationRecordShortViewModel : ScheduleRecordShortViewModel
    {
        public int ConsultationTime { get; set; }

        public string Text
		{
			get
			{
				return string.Format("{0} конс.{1}{2}{3}{2}{4}", LessonDiscipline, LessonClassroom, Environment.NewLine, LessonLecturer, LessonGroup);
			}
		}
	}

	/// <summary>
	/// Полная запись для редактирования
	/// </summary>
	public class ConsultationRecordViewModel : ScheduleRecordViewModel
    {
        public int ConsultationTime { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}{3}{4}{3}{5}", LessonType, LessonDiscipline, LessonClassroom, Environment.NewLine, LessonLecturer, LessonStudentGroup);
        }
    }
}