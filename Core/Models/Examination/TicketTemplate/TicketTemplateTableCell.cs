using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Examination
{
    public class TicketTemplateTableCell : BaseEntity
    {
        public Guid? TicketTemplateTableRowId { get; set; }

        public Guid? PropertiesId { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplateTableRow TicketTemplateTableRow { get; set; }

        public virtual TicketTemplateElementaryUnit Properties { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("TicketTemplateTableCellId")]
        public virtual List<TicketTemplateParagraph> TicketTemplateParagraphs { get; set; }

        //-------------------------------------------------------------------------
    }
}