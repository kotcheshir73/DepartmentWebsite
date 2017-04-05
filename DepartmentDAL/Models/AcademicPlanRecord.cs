using DepartmentDAL.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentDAL.Models
{
	/// <summary>
	/// Класс, хранящий информацию о записи учебного плана
	/// </summary>
	public class AcademicPlanRecord : BaseEntity
	{ 
		public long AcademicPlanId { get; set; }

		public long DisciplineId { get; set; }

		public long KindOfLoadId { get; set; }

		public Semesters Semester { get; set; }

		public int Hours { get; set; }

		public virtual AcademicPlan AcademicPlan { get; set; }

		public virtual Discipline Discipline { get; set; }

		public virtual KindOfLoad KindOfLoad { get; set; }

		[ForeignKey("AcademicPlanRecordId")]
		public virtual List<LoadDistributionRecord> LoadDistributionRecord { get; set; }
	}
}
