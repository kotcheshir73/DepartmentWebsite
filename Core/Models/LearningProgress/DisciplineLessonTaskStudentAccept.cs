using Enums;
using Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.LearningProgress
{
    /// <summary>
    /// Класс, хранящий информацию по сдаче студентом задания по занятию
    /// </summary>
    [DataContract]
    public class DisciplineLessonTaskStudentAccept : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid DisciplineLessonTaskId { get; set; }

        [Required]
        [DataMember]
        public Guid StudentId { get; set; }

        [Required]
        [DataMember]
        public DisciplineLessonTaskStudentResult Result { get; set; }

        [Required]
        [DataMember]
        public string Task { get; set; }

        [DataMember]
        public DateTime DateAccept { get; set; }

        [Required]
        [DataMember]
        public decimal Score { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public string Log { get; set; }

        //-------------------------------------------------------------------------

        public virtual DisciplineLessonTask DisciplineLessonTask { get; set; }

		public virtual Student Student { get; set; }

        //-------------------------------------------------------------------------
    }
}