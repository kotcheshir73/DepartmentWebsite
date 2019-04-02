using System;
using System.Runtime.Serialization;

namespace Models.Lecturer
{
    /// <summary>
    /// Класс, описывающий НИР(научные статьи) преподавателей
    /// </summary>
    [DataContract]
    public class IndividualPlanNIRContractualWork : BaseEntity
    {
        [DataMember]
        public Guid LecturerId { get; set; }
        
        [DataMember]
        public string JobContent { get; set; }

        [DataMember]
        public string Post { get; set; }

        [DataMember]
        public string PlannedTerm { get; set; }

        [DataMember]
        public string ReadyMark { get; set; }

        //-------------------------------------------------------------------------

        public virtual Base.Lecturer Lecturer { get; set; }

        //-------------------------------------------------------------------------
    }
}