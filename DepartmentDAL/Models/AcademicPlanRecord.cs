using DepartmentDAL.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, хранящий информацию о записи учебного плана
    /// </summary>
    [DataContract]
    public class AcademicPlanRecord : BaseEntity
    {
        [DataMember]
        public long AcademicPlanId { get; set; }

        [DataMember]
        public long DisciplineId { get; set; }

        [DataMember]
        public long KindOfLoadId { get; set; }

        [DataMember]
        public Semesters Semester { get; set; }

        [DataMember]
        public int Hours { get; set; }

		public virtual AcademicPlan AcademicPlan { get; set; }

		public virtual Discipline Discipline { get; set; }

		public virtual KindOfLoad KindOfLoad { get; set; }

		[ForeignKey("AcademicPlanRecordId")]
		public virtual List<LoadDistributionRecord> LoadDistributionRecord { get; set; }
	}
}
