using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentDAL.Models
{
	/// <summary>
	/// Класс, описывающий дисциплину
	/// </summary>
	public class Discipline : BaseEntity
	{
		[Display(Name = "Название дисциплины")]
		[MaxLength(200)]
		[Required]
		public string DisciplineName { get; set; }

        [Display(Name = "Краткое название дисциплины")]
        [MaxLength(20)]
        public string DisciplineShortName { get; set; }

        public long DisciplineBlockId { get; set; }

		public DisciplineBlock DisciplineBlock { get; set; }

		[ForeignKey("DisciplineId")]
		public virtual List<AcademicPlanRecord> AcademicPlanRecord { get; set; }

		[ForeignKey("DisciplineId")]
		public virtual List<DisciplineLesson> DisciplineLessons { get; set; }

		[ForeignKey("DisciplineId")]
		public virtual List<DisciplineStudentRecord> DisciplineStudentRecords { get; set; }
	}
}
