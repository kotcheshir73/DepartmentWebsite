using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentDAL.Models
{
    public class EducationDirection : BaseEntity
    {
        [Display(Name = "Код направления")]
        [MaxLength(10)]
        [Required]
        public string Cipher { get; set; }

        [Display(Name = "Название направления")]
        [MaxLength(100)]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        [Required]
        public string Description { get; set; }

        [ForeignKey("EducationDirectionId")]
        public virtual List<StudentGroup> StudentGroups { get; set; }

		[ForeignKey("EducationDirectionId")]
		public virtual List<Contingent> Contingents { get; set; }

		[ForeignKey("EducationDirectionId")]
		public virtual List<AcademicPlan> AcademicPlans { get; set; }

	}
}
