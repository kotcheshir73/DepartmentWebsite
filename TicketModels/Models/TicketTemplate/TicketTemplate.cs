using DepartmentModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace TicketModels.Models
{
    [DataContract]
    public class TicketTemplate : BaseEntity
    {
        [DataMember]
        public Guid? ExaminationTemplateId { get; set; }

        [DataMember]
        public string XML { get; set; }

        [DataMember]
        public string TemplateName { get; set; }

        //-------------------------------------------------------------------------

        public virtual ExaminationTemplate ExaminationTemplate { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("TicketTemplateId")]
        public virtual List<TicketTemplateBody> TicketTemplateBodies { get; set; }

        //-------------------------------------------------------------------------
    }
}