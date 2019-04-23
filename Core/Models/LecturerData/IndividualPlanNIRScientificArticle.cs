using Models.Base;
using System;
using System.Runtime.Serialization;

namespace Models.LecturerData
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

        public virtual Lecturer Lecturer { get; set; }

        //-------------------------------------------------------------------------
    }
}