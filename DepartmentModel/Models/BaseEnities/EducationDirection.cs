using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Направление обучения
    /// </summary>
    [DataContract]
    public class EducationDirection : BaseEntity
    {
        [MaxLength(10)]
        [Required]
        [DataMember]
        public string Cipher { get; set; }
        
        [MaxLength(100)]
        [Required]
        [DataMember]
        public string Title { get; set; }
        
        [Required]
        [DataMember]
        public string Description { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------

        [ForeignKey("EducationDirectionId")]
        public virtual List<StudentGroup> StudentGroups { get; set; }

		[ForeignKey("EducationDirectionId")]
		public virtual List<Contingent> Contingents { get; set; }

		[ForeignKey("EducationDirectionId")]
		public virtual List<AcademicPlan> AcademicPlans { get; set; }

	}
}
