using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, хранящий информацию по нормам времени
    /// </summary>
    [DataContract]
    public class TimeNorm : BaseEntity
	{
		[MaxLength(50)]
		[Required]
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public long KindOfLoadId { get; set; }

        [DataMember]
        public string Formula { get; set; }

        [DataMember]
        public decimal Hours { get; set; }

		public virtual KindOfLoad KindOfLoad { get; set; }

		[ForeignKey("TimeNormId")]
		public virtual List<LoadDistributionRecord> LoadDistributionRecord { get; set; }
	}
}
