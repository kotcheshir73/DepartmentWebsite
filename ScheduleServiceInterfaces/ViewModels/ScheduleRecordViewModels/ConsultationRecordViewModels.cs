using System;

namespace ScheduleServiceInterfaces.ViewModels
{
	/// <summary>
	/// Краткая запись для вывода в расписании
	/// </summary>
	public class ConsultationRecordShortViewModel : ScheduleRecordShortViewModel
    {
		public int Week { get; set; }

		public int Day { get; set; }

		public int Lesson { get; set; }

		public DateTime DateConsultation { get; set; }

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
		public DateTime DateConsultation { get; set; }
	}
}