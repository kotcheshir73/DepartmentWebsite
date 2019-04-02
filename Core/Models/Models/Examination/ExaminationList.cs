using Models.Base;
using Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Examination
{
    /// <summary>
    /// Класс, хранящий информацию о направлении на пересдачу по дисциплине
    /// </summary>
    [DataContract]
    public class ExaminationList : BaseEntity
    {
        [Required]
        [DataMember]
        public int Number { get; set; }

        [Required]
        [DataMember]
        public Guid LecturerId { get; set; }

        [Required]
        [DataMember]
        public Guid DisciplineId { get; set; }

        [Required]
        [DataMember]
        public Guid AcademicYearId { get; set; }

        [Required]
        [DataMember]
        public Guid StudentGroupId { get; set; }

        [Required]
        [DataMember]
        public Guid StudentId { get; set; }

        [Required]
        [DataMember]
        public TypeOfTest TypeOfTest { get; set; }

        [DataMember]
        public string Score { get; set; }

        //-------------------------------------------------------------------------

        public virtual Base.Lecturer Lecturer { get; set; }

        public virtual AcademicYear.AcademicYear AcademicYear { get; set; }

        public virtual Discipline Discipline { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }

        public virtual Student Student { get; set; }

        //-------------------------------------------------------------------------
    }
}