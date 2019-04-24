using System;
using System.Runtime.Serialization;

namespace Models.AcademicYearData
{
    /// <summary>
    /// Класс, описывающий Участие в хоздоговорной НИР преподавателей
    /// </summary>
    [DataContract]
    public class IndividualPlanNIRContractualWork : BaseEntity
    {
        [DataMember]
        public Guid IndividualPlanId { get; set; }

        [DataMember]
        public string JobContent { get; set; }

        [DataMember]
        public string Post { get; set; }

        [DataMember]
        public string PlannedTerm { get; set; }

        [DataMember]
        public bool ReadyMark { get; set; }

        //-------------------------------------------------------------------------

        public virtual IndividualPlan IndividualPlan { get; set; }

        //-------------------------------------------------------------------------
    }
}