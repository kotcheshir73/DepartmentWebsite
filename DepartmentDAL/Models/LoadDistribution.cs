using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, хранящий информацию по распределению нагрузки
    /// </summary>
    [DataContract]
    public class LoadDistribution : BaseEntity
    {
        [DataMember]
        public long AcademicYearId { get; set; }

		public virtual AcademicYear AcademicYear { get; set; }

		[ForeignKey("LoadDistributionId")]
		public virtual List<LoadDistributionRecord> LoadDistributionRecords { get; set; }
	}
}
