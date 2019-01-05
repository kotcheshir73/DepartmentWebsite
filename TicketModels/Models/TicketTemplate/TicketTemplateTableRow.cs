using DepartmentModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TicketModels.Models
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

        public override string ToString()
        {
            StringBuilder attributes = new StringBuilder();
            if (TicketTemplateElementaryAttributes != null)
            {
                foreach (var attr in TicketTemplateElementaryAttributes)
                {
                    attributes.Append(attr?.ToString() ?? string.Empty);
                }
            }
            StringBuilder nodes = new StringBuilder();
            if (TicketTemplateTableCells != null)
            {
                foreach (var node in TicketTemplateTableCells.OrderBy(x => x.Order))
                {
                    if (node != null)
                    {
                        nodes.Append(node?.ToString() ?? string.Empty);
                    }
                }
            }

            return string.Format("<{0}{1}>{2}{3}</{0}>", Name ?? string.Empty, attributes.ToString(), Properties?.ToString() ?? string.Empty, nodes.ToString());
        }
    }
}