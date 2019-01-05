using DepartmentModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TicketModels.Models
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
            if (TicketTemplateElementaryUnits != null)
            {
                foreach (var node in TicketTemplateElementaryUnits.OrderBy(x => x.Order))
                {
                    if(node != null)
                    {
                        nodes.Append(node?.ToString() ?? string.Empty);
                    }
                }
            }
            StringBuilder text = new StringBuilder();
            if (!string.IsNullOrEmpty(Text))
            {
                text.AppendFormat("<{0}>{1}</{0}>", TextName ?? string.Empty, Text ?? string.Empty);
            }

            return string.Format("<{0}{1}>{2}{3}{4}</{0}>", Name ?? string.Empty, attributes.ToString(), Font?.ToString() ?? string.Empty, nodes.ToString(), text.ToString());
        }
    }
}