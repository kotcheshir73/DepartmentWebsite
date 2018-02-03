using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, хранящий информацию по записи распределения нагрузки
    /// </summary>
    [DataContract]
    public class LoadDistributionRecord : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid LoadDistributionId { get; set; }

        [Required]
        [DataMember]
        public Guid AcademicPlanRecordId { get; set; }

        [Required]
        [DataMember]
        public Guid ContingentId { get; set; }

        [Required]
        [DataMember]
        public Guid TimeNormId { get; set; }

        [Required]
        [DataMember]
        public decimal Load { get; set; }

        //-------------------------------------------------------------------------

        public virtual LoadDistribution LoadDistribution { get; set; }

		public virtual AcademicPlanRecord AcademicPlanRecord { get; set; }

		public virtual Contingent Contingent { get; set; }

		public virtual TimeNorm TimeNorm { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("LoadDistributionRecordId")]
		public virtual List<LoadDistributionMission> LoadDistributionMissions { get; set; }
	}
}
