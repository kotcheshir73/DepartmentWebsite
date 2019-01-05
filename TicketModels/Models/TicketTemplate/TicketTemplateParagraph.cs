using DepartmentModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TicketModels.Models
{
    public class TicketTemplateParagraph : BaseEntity
    {
        public Guid? TicketTemplateBodyId { get; set; }

        public Guid? TicketTemplateTableCellId { get; set; }

        public Guid? ParagraphFormatId { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplateBody TicketTemplateBody { get; set; }

        public virtual TicketTemplateElementaryUnit ParagraphFormat { get; set; }

        public virtual TicketTemplateTableCell TicketTemplateTableCell { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("TicketTemplateParagraphId")]
        public virtual List<TicketTemplateElementaryAttribute> TicketTemplateElementaryAttributes { get; set; }

        [ForeignKey("TicketTemplateParagraphId")]
        public virtual List<TicketTemplateParagraphData> TicketTemplateParagraphDatas { get; set; }

        //-------------------------------------------------------------------------

        public override string ToString()
        {
            StringBuilder attributes = new StringBuilder();
            if (TicketTemplateElementaryAttributes != null)
            {
                foreach (var attr in TicketTemplateElementaryAttributes)
                {
                    attributes.Append(attr.ToString());
                }
            }
            StringBuilder nodes = new StringBuilder();
            if (TicketTemplateParagraphDatas != null)
            {
                foreach (var node in TicketTemplateParagraphDatas.OrderBy(x => x.Order))
                {
                    if (node != null)
                    {
                        nodes.Append(node.ToString());
                    }
                }
            }

            return string.Format("<{0}{1}>{2}{3}</{0}>", Name, attributes.ToString(), ParagraphFormat?.ToString(), nodes.ToString());
        }
    }
}