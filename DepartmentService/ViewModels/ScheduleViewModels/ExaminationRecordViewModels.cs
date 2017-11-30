using System;

namespace DepartmentService.ViewModels
{
	/// <summary>
	/// Краткая запись для вывода в расписании
	/// </summary>
	public class ExaminationRecordShortViewModel : ScheduleRecordShortViewModels
    {
		public DateTime DateConsultation { get; set; }

		public DateTime DateExamination { get; set; }

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
	public class ExaminationRecordViewModel : ScheduleRecordViewModels
    {
		public DateTime DateConsultation { get; set; }

		public DateTime DateExamination { get; set; }
	}
}
