using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Examination
{
    /// <summary>
    /// Класс отвечает за абзац в документе
    /// </summary>
    [DataContract]
    public class TicketTemplateParagraph : IdEntity
    {
        [DataMember]
        public Guid? TicketTemplateBodyId { get; set; }

        [DataMember]
        public Guid? TicketTemplateTableCellId { get; set; }

        [DataMember]
        public Guid? TicketTemplateParagraphPropertiesId { get; set; }

        [DataMember]
        public int Order { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplateBody TicketTemplateBody { get; set; }

        public virtual TicketTemplateTableCell TicketTemplateTableCell { get; set; }

        public virtual TicketTemplateParagraphProperties TicketTemplateParagraphProperties { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("TicketTemplateParagraphId")]
        public virtual List<TicketTemplateParagraphRun> TicketTemplateParagraphRuns { get; set; }

        //-------------------------------------------------------------------------
    }
}