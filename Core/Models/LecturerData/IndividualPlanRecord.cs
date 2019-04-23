using System;
using System.Runtime.Serialization;

namespace Models.LecturerData
{
    /// <summary>
    /// Класс, описывающий записи индивидуального плана
    /// </summary>
    [DataContract]
    public class IndividualPlanRecord : BaseEntity
    {
        [DataMember]
        public Guid IndividualPlanId { get; set; }

        [DataMember]
        public Guid IndividualPlanKindOfWorkId { get; set; }

        [DataMember]
        public double PlanAutumn { get; set; }

        [DataMember]
        public double FactAutumn { get; set; }

        [DataMember]
        public double PlanSpring { get; set; }

        [DataMember]
        public double FactSpring { get; set; }


        //-------------------------------------------------------------------------

        public virtual IndividualPlan IndividualPlan { get; set; }

        public virtual IndividualPlanKindOfWork IndividualPlanKindOfWorks { get; set; }

        //-------------------------------------------------------------------------
    }
}