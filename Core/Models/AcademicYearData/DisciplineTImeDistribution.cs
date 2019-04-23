using Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.AcademicYearData
{
    /// <summary>
    /// Класс, хранящий информацию о расчасовке
    /// </summary>
    [DataContract]
    public class DisciplineTimeDistribution : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid AcademicPlanRecordId { get; set; }

        [Required]
        [DataMember]
        public Guid StudentGroupId { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public string CommentWishesOfTeacher { get; set; }

        //-------------------------------------------------------------------------
               
        public virtual AcademicPlanRecord AcademicPlanRecord { get; set; }
        
        public virtual StudentGroup StudentGroup { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("DisciplineTimeDistributionId")]
        public virtual List<DisciplineTimeDistributionRecord> DisciplineTimeDistributionRecords { get; set; }

        [ForeignKey("DisciplineTimeDistributionId")]
        public virtual List<DisciplineTimeDistributionClassroom> DisciplineTimeDistributionClassrooms { get; set; }
    }
}
