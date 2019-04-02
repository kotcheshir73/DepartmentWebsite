using System;
using System.Runtime.Serialization;

namespace Models.Lecturer
{
    /// <summary>
    /// Класс, описывающий НИР(научные статьи) преподавателей
    /// </summary>
    [DataContract]
    public class IndividualPlanNIRScientificArticle : BaseEntity
    {
        [DataMember]
        public Guid LecturerId { get; set; }
        
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string TypeOfPublication { get; set; }

        [DataMember]
        public string Volume { get; set; }

        [DataMember]
        public string Publishing { get; set; }

        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public string Status { get; set; }

        //-------------------------------------------------------------------------

        public virtual Base.Lecturer Lecturer { get; set; }

        //-------------------------------------------------------------------------
    }
}