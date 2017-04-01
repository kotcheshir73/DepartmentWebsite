using DepartmentDAL.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentDAL.Models
{
	/// <summary>
	/// Класс, описывающий учебный план
	/// </summary>
	public class AcademicPlan : BaseEntity
	{
		public long EducationDirectionId { get; set; }

		public long AcademicYearId { get; set; }

		public AcademicLevel AcademicLevel { get; set; }

		public AcademicCourse AcademicCourses { get; set; }

		public AcademicYear AcademicYear { get; set; }

		public virtual EducationDirection EducationDirection { get; set; }

		[ForeignKey("AcademicPlanId")]
		public virtual List<AcademicPlanRecord> AcademicPlanRecords { get; set; }
	}
}
