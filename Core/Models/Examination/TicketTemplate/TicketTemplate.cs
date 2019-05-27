using System;
using System.Runtime.Serialization;

namespace Models.Examination
{
    [DataContract]
    public class TicketTemplate : BaseEntity
    {
        [DataMember]
        public Guid? ExaminationTemplateId { get; set; }

        [DataMember]
        public Guid? TicketTemplateBodyId { get; set; }

        [DataMember]
        public string TemplateName { get; set; }

        //-------------------------------------------------------------------------

        public virtual ExaminationTemplate ExaminationTemplate { get; set; }

        public virtual TicketTemplateBody TicketTemplateBody { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------
    }
}