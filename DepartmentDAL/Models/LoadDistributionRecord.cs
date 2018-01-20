using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, хранящий информацию по записи распределения нагрузки
    /// </summary>
    [DataContract]
    public class LoadDistributionRecord : BaseEntity
    {
        [DataMember]
        public long LoadDistributionId { get; set; }

        [DataMember]
        public long AcademicPlanRecordId { get; set; }

        [DataMember]
        public long ContingentId { get; set; }

        [DataMember]
        public long TimeNormId { get; set; }

        [DataMember]
        public decimal Load { get; set; }

		public virtual LoadDistribution LoadDistribution { get; set; }

		public virtual AcademicPlanRecord AcademicPlanRecord { get; set; }

		public virtual Contingent Contingent { get; set; }

		public virtual TimeNorm TimeNorm { get; set; }

		[ForeignKey("LoadDistributionRecordId")]
		public virtual List<LoadDistributionMission> LoadDistributionMissions { get; set; }
	}
}
