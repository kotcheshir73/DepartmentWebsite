using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.AcademicYearData
{
    /// <summary>
    /// Класс, описывающий даты для учебного полугодья
    /// </summary>
    [DataContract]
    public class SeasonDates : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid AcademicYearId { get; set; }

        [Required]
        [DataMember]
        public string Title { get; set; }

        [Required]
        [DataMember]
        public DateTime DateBeginFirstHalfSemester { get; set; }

        [Required]
        [DataMember]
        public DateTime DateEndFirstHalfSemester { get; set; }

        [Required]
        [DataMember]
        public DateTime DateBeginSecondHalfSemester { get; set; }

        [Required]
        [DataMember]
        public DateTime DateEndSecondHalfSemester { get; set; }

        [Required]
        [DataMember]
        public DateTime DateBeginOffset { get; set; }

        [Required]
        [DataMember]
        public DateTime DateEndOffset { get; set; }

        [Required]
        [DataMember]
        public DateTime DateBeginExamination { get; set; }

        [Required]
        [DataMember]
        public DateTime DateEndExamination { get; set; }

        [DataMember]
        public DateTime? DateBeginPractice { get; set; }

        [DataMember]
        public DateTime? DateEndPractice { get; set; }

        //-------------------------------------------------------------------------

        public virtual AcademicYear AcademicYear { get; set; }

        //-------------------------------------------------------------------------
    }
}
