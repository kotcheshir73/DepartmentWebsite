using DepartmentModel.Enums;
using DepartmentModel.Models.BaseEnities;
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
        public Guid DisciplineLessonRecordId { get; set; }

        [Required]
        [DataMember]
        public Guid StudentId { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [Required]
        [DataMember]
        public DisciplineLessonStudentStatus Status { get; set; }
        
        [DataMember]
        public int? Ball { get; set; }

        //-------------------------------------------------------------------------

        public virtual DisciplineLessonRecord DisciplineLessonRecord { get; set; }

		public virtual Student Student { get; set; }

        //-------------------------------------------------------------------------
    }
}
