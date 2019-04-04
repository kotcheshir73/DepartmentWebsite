using Models.Attributes;
using Models.Base;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.AcademicYearData
{
    /// <summary>
    /// Класс, описывающий учебный план
    /// </summary>
    [DataContract]
    [ClassUse("AcademicYear", "AcademicYearId", "Учебный план подгружается на учебный год")]
    [ClassUse("EducationDirection", "EducationDirectionId", "Учебный план привязан к направлению подготовки")]
    public class AcademicPlan : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid AcademicYearId { get; set; }

        [DataMember]
        public Guid? EducationDirectionId { get; set; }

        [Required]
        [DataMember]
        public AcademicLevel AcademicLevel { get; set; }
        
        [DataMember]
        public AcademicCourse? AcademicCourses { get; set; }

        //-------------------------------------------------------------------------

		public virtual AcademicYear AcademicYear { get; set; }
        
		public virtual EducationDirection EducationDirection { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("AcademicPlanId")]
		public virtual List<AcademicPlanRecord> AcademicPlanRecords { get; set; }
	}
}