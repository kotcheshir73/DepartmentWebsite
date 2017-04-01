using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentDAL.Models
{
	/// <summary>
	/// Класс, хранящий инорфмацию по учебным годам
	/// </summary>
	public class AcademicYear: BaseEntity
	{
		[Display(Name = "Учебный год")]
		[MaxLength(10)]
		[Required]
		public string Title { get; set; }

		[ForeignKey("AcademicYearId")]
		public virtual List<AcademicPlan> AcademicPlans { get; set; }
	}
}
