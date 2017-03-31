using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentDAL.Models
{
    public class Discipline : BaseEntity
	{

		[Display(Name = "Название дисциплины")]
		[MaxLength(100)]
		[Required]
		public string DisciplineName { get; set; }

		[ForeignKey("DisciplineId")]
		public virtual List<AcademicPlanRecord> AcademicPlanRecord { get; set; }
	}
}
