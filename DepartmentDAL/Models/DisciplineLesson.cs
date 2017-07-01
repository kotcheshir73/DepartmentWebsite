using DepartmentDAL.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentDAL.Models
{
	/// <summary>
	///  Класс, описывающий занятие по дисциплине
	///  Это может быть лекция, практическое занятие или лабораторная работа
	/// </summary>
	public class DisciplineLesson : BaseEntity
	{
		[Required]
		public LessonTypes LessonType { get; set; }

		[Display(Name = "Тема занятия")]
		[MaxLength(100)]
		[Required]
		public string Title { get; set; }

		[Display(Name = "Описание занятия")]
		public string Description { get; set; }

		public long DisciplineId { get; set; }

		public virtual Discipline Discipline { get; set; }

		[ForeignKey("DisciplineLessonId")]
		public virtual List<DisciplineLessonTask> DisciplineLessonTasks { get; set; }

		[ForeignKey("DisciplineLessonId")]
		public virtual List<DisciplineLessonStudentRecord> DisciplineLessonStudentRecords { get; set; }
	}
}
