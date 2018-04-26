using DepartmentModel.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, описывающий посещение занятия студентом
    /// </summary>
    [DataContract]
    public class DisciplineLessonStudentRecord : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid DisciplineLessonId { get; set; }

        [Required]
        [DataMember]
        public Guid StudentId { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [Required]
        [DataMember]
        public DisciplineLessonStudentStatus Status { get; set; }

        //-------------------------------------------------------------------------

        public virtual DisciplineLesson DisciplineLesson { get; set; }

		public virtual Student Student { get; set; }

        //-------------------------------------------------------------------------
    }
}
