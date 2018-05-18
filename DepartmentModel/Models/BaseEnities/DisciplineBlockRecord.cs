using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Записи для расчетов штата, не вытаскиваемые из планов
    /// </summary>
    [DataContract]
    public class DisciplineBlockRecord : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid DisciplineBlockId { get; set; }
        
        [DataMember]
        public Guid? EducationDirectionId { get; set; }

        [Required]
        [DataMember]
        public Guid AcademicYearId { get; set; }

        [Required]
        [DataMember]
        public Guid TimeNormId { get; set; }

        [Required]
        [DataMember]
        public string DisciplineBlockRecordTitle { get; set; }

        [Required]
        [DataMember]
        public decimal DisciplineBlockRecordHours { get; set; }

        //-------------------------------------------------------------------------

        public virtual DisciplineBlock DisciplineBlock { get; set; }

        public virtual EducationDirection EducationDirection { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual TimeNorm TimeNorm { get; set; }
    }
}
