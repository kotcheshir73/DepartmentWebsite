using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Examination
{
    [DataContract]
    public class TicketTemplate : BaseEntity
    {
        [DataMember]
        public Guid? TicketTemplateBodyId { get; set; }

        [DataMember]
        public string TemplateName { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplateBody TicketTemplateBody { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("ExaminationTemplateId")]
        public virtual List<ExaminationTemplate> ExaminationTemplates { get; set; }

        //-------------------------------------------------------------------------
    }
}