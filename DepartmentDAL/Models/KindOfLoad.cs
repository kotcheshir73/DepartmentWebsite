using DepartmentDAL.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentDAL.Models
{
	/// <summary>
	/// Класс, фиксирующий виды нагрузок
	/// </summary>
	public class KindOfLoad : BaseEntity
	{
		[Display(Name = "Вид нагрузки")]
		[MaxLength(50)]
		[Required]
		public string KindOfLoadName { get; set; }

		public KindOfLoadType KindOfLoadType { get; set; }

		[ForeignKey("KindOfLoadId")]
		public virtual List<AcademicPlanRecord> AcademicPlanRecords { get; set; }

		[ForeignKey("KindOfLoadId")]
		public virtual List<TimeNorm> TimeNorms { get; set; }
	}
}
