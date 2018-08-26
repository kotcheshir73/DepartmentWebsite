using DepartmentModel.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, хранящий информацию по сдаче студентом задания по занятию
    /// </summary>
    [DataContract]
    public class DisciplineLessonTaskStudentRecord : BaseEntity
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

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public DateTime? Date { get; set; }

        [Required]
        [DataMember]
        public int Score { get; set; }

        //-------------------------------------------------------------------------

        public virtual DisciplineLessonTask DisciplineLessonTask { get; set; }

		public virtual Student Student { get; set; }

        //-------------------------------------------------------------------------
    }
}
