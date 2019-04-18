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
    public class DisciplineLessonConductedStudent : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid DisciplineLessonConductedId { get; set; }

        [Required]
        [DataMember]
        public Guid StudentId { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [Required]
        [DataMember]
        public DisciplineLessonStudentStatus Status { get; set; }

        [DataMember]
        public decimal? Ball { get; set; }

        //-------------------------------------------------------------------------

        public virtual DisciplineLessonConducted DisciplineLessonConducted { get; set; }

        public virtual Student Student { get; set; }

        //-------------------------------------------------------------------------
    }
}
