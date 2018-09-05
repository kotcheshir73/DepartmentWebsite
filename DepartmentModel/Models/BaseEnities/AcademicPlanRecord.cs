using DepartmentModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, хранящий информацию о дисциплине в учебном плане
    /// </summary>
    [DataContract]
    public class AcademicPlanRecord : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid AcademicPlanId { get; set; }

        [Required]
        [DataMember]
        public Guid DisciplineId { get; set; }
        
        [DataMember]
        public Guid? ContingentId { get; set; }
        
        [DataMember]
        public Semesters? Semester { get; set; }

        [Required]
        [DataMember]
        public int Zet { get; set; }

        //-------------------------------------------------------------------------

        public virtual AcademicPlan AcademicPlan { get; set; }

		public virtual Discipline Discipline { get; set; }

        public virtual Contingent Contingent { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("AcademicPlanRecordId")]
        public virtual List<AcademicPlanRecordElement> AcademicPlanRecordElements { get; set; }

        [ForeignKey("AcademicPlanRecordId")]
        public virtual List<Statement> Statement { get; set; }
    }
}
