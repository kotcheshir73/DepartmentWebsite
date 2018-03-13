using DepartmentModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, хранящий информацию о записи учебного плана
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

        /*[Required]
        [DataMember]
        public Guid KindOfLoadId { get; set; }*/

        [Required]
        [DataMember]
        public Semesters Semester { get; set; }

        /*[Required]
        [DataMember]
        public int Hours { get; set; }*/

        [Required]
        [DataMember]
        public int Zet { get; set; }

        //-------------------------------------------------------------------------

        public virtual AcademicPlan AcademicPlan { get; set; }

		public virtual Discipline Discipline { get; set; }

		//public virtual KindOfLoad KindOfLoad { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("AcademicPlanRecordId")]
		public virtual List<LoadDistributionRecord> LoadDistributionRecord { get; set; }
	}
}
