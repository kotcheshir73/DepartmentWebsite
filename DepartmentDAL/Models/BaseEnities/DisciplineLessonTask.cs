using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий задачи к занятиям дисциплины
    /// Это или лабораторная работа или практическое занятие
    /// </summary>
    [DataContract]
    public class DisciplineLessonTask : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid DisciplineLessonId { get; set; }

        [Required]
        [DataMember]
        public int VariantNumber { get; set; }

        [Required]
        [DataMember]
        public int Order { get; set; }
        
        [DataMember]
        public decimal? MaxBall { get; set; }

        //-------------------------------------------------------------------------

        public virtual DisciplineLesson DisciplineLesson { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("DisciplineLessonTaskId")]
		public virtual List<DisciplineLessonTaskImageContext> DisciplineLessonTaskImageContexts { get; set; }

		[ForeignKey("DisciplineLessonTaskId")]
		public virtual List<DisciplineLessonTaskTextContext> DisciplineLessonTaskTextContexts { get; set; }

		[ForeignKey("DisciplineLessonTaskId")]
		public virtual List<DisciplineLessonTaskStudentRecord> DisciplineLessonTaskStudentRecords { get; set; }
	}
}
