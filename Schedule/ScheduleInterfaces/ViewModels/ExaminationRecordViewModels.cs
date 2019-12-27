using System;

namespace ScheduleInterfaces.ViewModels
{
	/// <summary>
	/// Краткая запись для вывода в расписании
	/// </summary>
	public class ExaminationRecordShortViewModel : ScheduleRecordShortViewModel
    {
		public DateTime DateConsultation { get; set; }

        public string LessonConsultationClassroom { get; set; }

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
	public class ExaminationRecordViewModel : ScheduleRecordViewModel
    {
		public DateTime DateConsultation { get; set; }

        public string LessonConsultationClassroom { get; set; }

        public Guid? ConsultationClassroomId { get; set; }

        public string ConsultationClassroom { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}({3}){4}{5}{4}{6}", LessonType, LessonDiscipline, LessonClassroom, LessonConsultationClassroom, Environment.NewLine, LessonLecturer, LessonStudentGroup);
        }
    }
}