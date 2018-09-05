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
    public class IndividualPlanRecord : BaseEntity
    {
        [DataMember]
        public Guid IndividualPlanKindOfWorkId { get; set; }

        [DataMember]
        public Guid LecturerId { get; set; }

        [DataMember]
        public double PlanAutumn { get; set; }

        [DataMember]
        public double FactAutumn { get; set; }

        [DataMember]
        public double PlanSpring { get; set; }

        [DataMember]
        public double FactSpring { get; set; }


        //-------------------------------------------------------------------------
        public virtual Lecturer Lecturer { get; set; }
        public virtual IndividualPlanKindOfWork IndividualPlanKindOfWorks { get; set; }
        //-------------------------------------------------------------------------
    }
}

