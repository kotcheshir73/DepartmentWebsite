using Models.Base;
using Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.LearningProgress
{
    /// <summary>
    /// Информация по студенту в рамках дисциплины
    /// В какой подгруппе, какой у него вариант
    /// </summary>
    [DataContract]
    public class DisciplineStudentRecord : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid DisciplineId { get; set; }

        [Required]
        [DataMember]
        public Guid StudentId { get; set; }

        [Required]
        [DataMember]
        public Semesters Semester { get; set; }

        [Required]
        [DataMember]
        public string Variant { get; set; }

        [DataMember]
        public int SubGroup { get; set; }

        //-------------------------------------------------------------------------

        public virtual Discipline Discipline { get; set; }

		public virtual Student Student { get; set; }

        //-------------------------------------------------------------------------
    }
}