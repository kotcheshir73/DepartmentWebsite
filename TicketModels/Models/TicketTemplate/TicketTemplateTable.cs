using DepartmentModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TicketModels.Models
{
    public class TicketTemplateTable : BaseEntity
    {
        public Guid? TicketTemplateBodyId { get; set; }

        public Guid? PropertiesId { get; set; }

        public Guid? ColumnsId { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplateBody TicketTemplateBody { get; set; }

        public virtual TicketTemplateElementaryUnit Properties { get; set; }

        public virtual TicketTemplateElementaryUnit Columns { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("TicketTemplateTableId")]
        public virtual List<TicketTemplateTableRow> TicketTemplateTableRows { get; set; }

        //-------------------------------------------------------------------------

        public override string ToString()
        {
            StringBuilder nodes = new StringBuilder();
            if (TicketTemplateTableRows != null)
            {
                foreach (var node in TicketTemplateTableRows.OrderBy(x => x.Order))
                {
                    if (node != null)
                    {
                        nodes.Append(node?.ToString() ?? string.Empty);
                    }
                }
            }

            return string.Format("<{0}>{1}{2}{3}</{0}>", Name ?? string.Empty, Properties?.ToString() ?? string.Empty, Columns?.ToString() ?? string.Empty, nodes.ToString());
        }
    }
}