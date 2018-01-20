using DepartmentDAL.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    ///  Класс, описывающий занятие по дисциплине
    ///  Это может быть лекция, практическое занятие или лабораторная работа
    /// </summary>
    [DataContract]
    public class DisciplineLesson : BaseEntity
	{
		[Required]
        [DataMember]
        public LessonTypes LessonType { get; set; }
        
		[MaxLength(100)]
		[Required]
        [DataMember]
        public string Title { get; set; }
        
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public long DisciplineId { get; set; }

		public virtual Discipline Discipline { get; set; }

		[ForeignKey("DisciplineLessonId")]
		public virtual List<DisciplineLessonTask> DisciplineLessonTasks { get; set; }

		[ForeignKey("DisciplineLessonId")]
		public virtual List<DisciplineLessonStudentRecord> DisciplineLessonStudentRecords { get; set; }
	}
}
