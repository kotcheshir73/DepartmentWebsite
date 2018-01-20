using DepartmentDAL.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий учебный план
    /// </summary>
    [DataContract]
    public class AcademicPlan : BaseEntity
	{
        [DataMember]
        public long EducationDirectionId { get; set; }

        [DataMember]
        public long AcademicYearId { get; set; }

        [DataMember]
        public AcademicLevel AcademicLevel { get; set; }

        [DataMember]
        public AcademicCourse AcademicCourses { get; set; }

		public AcademicYear AcademicYear { get; set; }

		public virtual EducationDirection EducationDirection { get; set; }

		[ForeignKey("AcademicPlanId")]
		public virtual List<AcademicPlanRecord> AcademicPlanRecords { get; set; }
	}
}
