using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, хранящий информацию о нагрузке по дисциплине в учебном плане
    /// </summary>
    [DataContract]
    public class AcademicPlanRecordElement : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid AcademicPlanRecordId { get; set; }

        [Required]
        [DataMember]
        public Guid TimeNormId { get; set; }

        [Required]
        [DataMember]
        public decimal PlanHours { get; set; }

        [Required]
        [DataMember]
        public decimal FactHours { get; set; }

        //-------------------------------------------------------------------------

        public virtual AcademicPlanRecord AcademicPlanRecord { get; set; }

        public virtual TimeNorm TimeNorm { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("AcademicPlanRecordElementId")]
        public virtual List<StreamLessonRecord> StreamLessonRecords { get; set; }
    }
}
