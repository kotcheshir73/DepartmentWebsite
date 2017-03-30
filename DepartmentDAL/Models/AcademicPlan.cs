using System.ComponentModel.DataAnnotations;

namespace DepartmentDAL.Models
{
	/// <summary>
	/// Класс, описывающий учебный план
	/// </summary>
	public class AcademicPlan : BaseEntity
	{
		[Display(Name = "Учебный год")]
		[MaxLength(10)]
		[Required]
		public string AcademicYear { get; set; }

		public long EducationDirectionId { get; set; }

		public virtual EducationDirection EducationDirection { get; set; }
	}
}
