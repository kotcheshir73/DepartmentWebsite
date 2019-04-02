using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Lecturer
{
    /// <summary>
    /// Класс, описывающий виды работ индивидуального плана
    /// </summary>
    [DataContract]
    public class IndividualPlanKindOfWork : BaseEntity
    {
        [DataMember]
        public Guid IndividualPlanTitleId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Order { get; set; }

        [DataMember]
        public string TimeNormDescription { get; set; }

        //-------------------------------------------------------------------------

        public virtual IndividualPlanTitle IndividualPlanTitle { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("IndividualPlanKindOfWorkId")]
		public virtual List<IndividualPlanRecord> IndividualPlanRecords { get; set; }
	}
}