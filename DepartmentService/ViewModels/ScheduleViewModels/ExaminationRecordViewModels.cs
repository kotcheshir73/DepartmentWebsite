using System;

namespace DepartmentService.ViewModels
{
	/// <summary>
	/// Краткая запись для вывода в расписании
	/// </summary>
	public class ExaminationRecordShortViewModel
	{
		public long Id { get; set; }

		public DateTime DateConsultation { get; set; }

		public DateTime DateExamination { get; set; }

		public string NotParseRecord { get; set; }

		public string LessonDiscipline { get; set; }

		public string LessonLecturer { get; set; }

		public string LessonGroup { get; set; }

		public string LessonClassroom { get; set; }

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
	public class ExaminationRecordViewModel
	{
		public long Id { get; set; }

		public DateTime DateConsultation { get; set; }

		public DateTime DateExamination { get; set; }

		public string NotParseRecord { get; set; }

		public string LessonDiscipline { get; set; }

		public string LessonLecturer { get; set; }

		public string LessonGroup { get; set; }

		public string LessonClassroom { get; set; }

		public string ClassroomId { get; set; }

		public string Classroom { get; set; }

		public long? LecturerId { get; set; }

		public string Lecturer { get; set; }

		public string Discipline { get; set; }

		public long? StudentGroupId { get; set; }

		public string StudentGroup { get; set; }
	}
}
