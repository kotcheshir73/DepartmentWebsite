using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, фиксирующий виды нагрузок
    /// </summary>
    [DataContract]
    public class KindOfLoad : BaseEntity
	{
		[MaxLength(50)]
		[Required]
        [DataMember]
        public string KindOfLoadName { get; set; }

        [MaxLength(10)]
        [Required]
        [DataMember]
        public string AttributeName { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------

        [ForeignKey("KindOfLoadId")]
        public virtual List<AcademicPlanRecordElement> AcademicPlanRecordElements { get; set; }

        [ForeignKey("KindOfLoadId")]
		public virtual List<TimeNorm> TimeNorms { get; set; }
	}
}
