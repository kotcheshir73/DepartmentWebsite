using System;
using System.Runtime.Serialization;

namespace Models.AcademicYearData
{
    /// <summary>
    /// Класс, описывающий НИР(научные статьи) преподавателей
    /// </summary>
    [DataContract]
    public class IndividualPlanNIRScientificArticle : BaseEntity
    {
        [DataMember]
        public Guid IndividualPlanId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Order { get; set; }

        [DataMember]
        public string TypeOfPublication { get; set; }

        [DataMember]
        public double Volume { get; set; }

        [DataMember]
        public string Publishing { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public string Status { get; set; }

        //-------------------------------------------------------------------------

        public virtual IndividualPlan IndividualPlan { get; set; }

        //-------------------------------------------------------------------------
    }
}