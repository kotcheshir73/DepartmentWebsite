﻿using DepartmentModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
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
        public string TimeNormDescription { get; set; }

        //-------------------------------------------------------------------------
        public virtual IndividualPlanTitle IndividualPlanTitle { get; set; }
        //-------------------------------------------------------------------------

        [ForeignKey("IndividualPlanKindOfWorkId")]
		public virtual List<IndividualPlanRecord> IndividualPlanRecords { get; set; }
	}
}