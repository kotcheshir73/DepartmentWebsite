using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Examination
{
    public class TicketTemplateTableRow : BaseEntity
    {
        public Guid? TicketTemplateTableId { get; set; }

        public Guid? PropertiesId { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplateTable TicketTemplateTable { get; set; }

        public virtual TicketTemplateElementaryUnit Properties { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("TicketTemplateTableRowId")]
        public virtual List<TicketTemplateElementaryAttribute> TicketTemplateElementaryAttributes { get; set; }

        [ForeignKey("TicketTemplateTableRowId")]
        public virtual List<TicketTemplateTableCell> TicketTemplateTableCells { get; set; }

        //-------------------------------------------------------------------------
    }
}