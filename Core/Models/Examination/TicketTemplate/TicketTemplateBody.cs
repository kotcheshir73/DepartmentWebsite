using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Examination
{
    public class TicketTemplateBody : BaseEntity
    {
        public Guid? BodyFormatId { get; set; }

        public Guid? TicketTemplateId { get; set; }

        public string BodyName { get; set; }

        public string SectName { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplate TicketTemplate { get; set; }

        public virtual TicketTemplateElementaryUnit BodyFormat { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("TicketTemplateBodyId")]
        public virtual List<TicketTemplateParagraph> TicketTemplateParagraphs { get; set; }

        [ForeignKey("TicketTemplateBodyId")]
        public virtual List<TicketTemplateTable> TicketTemplateTables { get; set; }

        //-------------------------------------------------------------------------
    }
}