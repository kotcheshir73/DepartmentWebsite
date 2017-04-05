using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentDAL.Models
{
	/// <summary>
	/// Класс, хранящий информацию по распределению нагрузки
	/// </summary>
	public class LoadDistribution : BaseEntity
	{
		public long AcademicYearId { get; set; }

		public AcademicYear AcademicYear { get; set; }

		[ForeignKey("LoadDistributionId")]
		public virtual List<LoadDistributionRecord> LoadDistributionRecords { get; set; }
	}
}
