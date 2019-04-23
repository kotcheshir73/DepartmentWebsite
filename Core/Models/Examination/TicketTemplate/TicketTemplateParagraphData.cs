using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Examination
{
    public class TicketTemplateParagraphData : BaseEntity
    {
        public Guid? TicketTemplateParagraphId { get; set; }

        public Guid? FontId { get; set; }

        public string Name { get; set; }

        public string TextName { get; set; }

        public string Text { get; set; }

        public int Order { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplateParagraph TicketTemplateParagraph { get; set; }

        public virtual TicketTemplateElementaryUnit Font { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("TicketTemplateParagraphDataId")]
        public virtual List<TicketTemplateElementaryAttribute> TicketTemplateElementaryAttributes { get; set; }

        [ForeignKey("TicketTemplateParagraphDataId")]
        public virtual List<TicketTemplateElementaryUnit> TicketTemplateElementaryUnits { get; set; }

        //-------------------------------------------------------------------------
    }
}