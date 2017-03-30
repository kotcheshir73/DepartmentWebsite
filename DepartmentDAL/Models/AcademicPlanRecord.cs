using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentDAL.Models
{
	public class AcademicPlanRecord : BaseEntity
	{ 
		public long AcademicPlanId { get; set; }

		public long DisciplineId { get; set; }

		public virtual AcademicPlan AcademicPlan { get; set; }

		public virtual Discipline Discipline { get; set; }
	}
}
