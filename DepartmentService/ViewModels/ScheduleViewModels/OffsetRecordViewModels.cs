﻿using System;

namespace DepartmentService.ViewModels
{
	/// <summary>
	/// Краткая запись для вывода в расписании
	/// </summary>
	public class OffsetRecordShortViewModel : ScheduleRecordShortViewModels
    {
		public int Week { get; set; }

		public int Day { get; set; }

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
	public class OffsetRecordViewModel : ScheduleRecordViewModels
    {
		public int Week { get; set; }

		public int Day { get; set; }

		public int Lesson { get; set; }
	}
}