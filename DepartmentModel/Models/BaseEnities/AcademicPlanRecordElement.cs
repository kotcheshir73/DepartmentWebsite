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
    public class AcademicPlanRecordElement : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid AcademicPlanRecordId { get; set; }

        [Required]
        [DataMember]
        public Guid KindOfLoadId { get; set; }

        [Required]
        [DataMember]
        public decimal Hours { get; set; }

        //-------------------------------------------------------------------------

        public virtual AcademicPlanRecord AcademicPlanRecord { get; set; }

        public virtual KindOfLoad KindOfLoad { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("AcademicPlanRecordElementId")]
        public virtual List<StreamLessonRecord> StreamLessonRecords { get; set; }
    }
}
