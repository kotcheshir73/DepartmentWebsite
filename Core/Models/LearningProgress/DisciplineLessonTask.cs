using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.LearningProgress
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
        public string Task { get; set; }

        [Required]
        [DataMember]
        public bool IsNecessarily { get; set; }

        [Required]
        [DataMember]
        public int Order { get; set; }
        
        [DataMember]
        public decimal? MaxBall { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public byte?[] Image { get; set; }

        //-------------------------------------------------------------------------

        public virtual DisciplineLesson DisciplineLesson { get; set; }

        //-------------------------------------------------------------------------

		[ForeignKey("DisciplineLessonTaskId")]
		public virtual List<DisciplineLessonTaskStudentAccept> DisciplineLessonTaskStudentRecords { get; set; }

        [ForeignKey("DisciplineLessonTaskId")]
        public virtual List<DisciplineLessonTaskVariant> DisciplineLessonTaskVariant { get; set; }
    }
}