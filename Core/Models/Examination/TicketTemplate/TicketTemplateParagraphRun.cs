using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Examination
{
    /// <summary>
    /// Строка в документе
    /// </summary>
    [DataContract]
    public class TicketTemplateParagraphRun : IdEntity
    {
        [DataMember]
        public Guid TicketTemplateParagraphId { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public bool TabChar { get; set; }

        [DataMember]
        public bool Break { get; set; }

        [DataMember]
        public string BreakType { get; set; }

        [DataMember]
        public int Order { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplateParagraph TicketTemplateParagraph { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("TicketTemplateParagraphRunId")]
        public virtual List<TicketTemplateParagraphRunProperties> TicketTemplateParagraphRunProperties { get; set; }
    }
}