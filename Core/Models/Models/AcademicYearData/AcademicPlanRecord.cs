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
    /// Класс, хранящий информацию о дисциплине в учебном плане
    /// </summary>
    [DataContract]
    [ClassUse("AcademicPlan", "AcademicPlanId", "Запись учебного плана привязывается к плану")]
    [ClassUse("Discipline", "DisciplineId", "Запись учебного плана создается под конкретную дисциплину в конкретном семестре")]
    [ClassUse("Contingent", "ContingentId", "Запись учебного плана расчитывается на конкретный контингентs")]
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
        public virtual List<DisciplineTimeDistribution> DisciplineTimeDistributions { get; set; }
    }
}