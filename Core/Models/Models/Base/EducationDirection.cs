using Models.AcademicYearData;
using Models.Examination;
using Models.LearningProgress;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Base
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

        [MaxLength(10)]
        [Required]
        [DataMember]
        public string ShortName { get; set; }

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

        [ForeignKey("EducationDirectionId")]
        public virtual List<DisciplineLesson> DisciplineLessons { get; set; }

        [ForeignKey("EducationDirectionId")]
        public virtual List<ExaminationTemplate> ExaminationTemplates { get; set; }

    }
}
