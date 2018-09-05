using DepartmentModel.Enums;
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
    public class IndividualPlanTitle : BaseEntity
    {

        [DataMember]
        public string Title { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------

        [ForeignKey("IndividualPlanTitleId")]
		public virtual List<IndividualPlanKindOfWork> IndividualPlanKindOfWorks { get; set; }
	}
}
