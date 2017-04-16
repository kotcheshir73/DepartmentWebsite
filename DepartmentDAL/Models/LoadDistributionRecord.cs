using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentDAL.Models
{
	/// <summary>
	/// Класс, хранящий информацию по записи распределения нагрузки
	/// </summary>
	public class LoadDistributionRecord : BaseEntity
	{
		public long LoadDistributionId { get; set; }

		public long AcademicPlanRecordId { get; set; }

		public long ContingentId { get; set; }

		public long TimeNormId { get; set; }

		public decimal Load { get; set; }

		public virtual LoadDistribution LoadDistribution { get; set; }

		public virtual AcademicPlanRecord AcademicPlanRecord { get; set; }

		public virtual Contingent Contingent { get; set; }

		public virtual TimeNorm TimeNorm { get; set; }

		[ForeignKey("LoadDistributionRecordId")]
		public virtual List<LoadDistributionMission> LoadDistributionMissions { get; set; }
	}
}
