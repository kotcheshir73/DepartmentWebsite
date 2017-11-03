using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentDAL.Models
{
	/// <summary>
	/// Класс, описывающий задачи к занятиям дисциплины
	/// Это или лабораторная работа или практическое занятие
	/// </summary>
	public class DisciplineLessonTask : BaseEntity
	{
		public int VariantNumber { get; set; }

		public int Order { get; set; }

		[Display(Name = "Максимальный балл, который можно получить за занятие")]
		public decimal? MaxBall { get; set; }

		public long DisciplineLessonId { get; set; }

		public virtual DisciplineLesson DisciplineLesson { get; set; }

		[ForeignKey("DisciplineLessonTaskId")]
		public virtual List<DisciplineLessonTaskImageContext> DisciplineLessonTaskImageContexts { get; set; }

		[ForeignKey("DisciplineLessonTaskId")]
		public virtual List<DisciplineLessonTaskTextContext> DisciplineLessonTaskTextContexts { get; set; }

		[ForeignKey("DisciplineLessonTaskId")]
		public virtual List<DisciplineLessonTaskStudentRecord> DisciplineLessonTaskStudentRecords { get; set; }
	}
}
