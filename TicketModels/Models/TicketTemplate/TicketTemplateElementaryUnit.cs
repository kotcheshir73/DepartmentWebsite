using DepartmentModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TicketModels.Models
{
    public class TicketTemplateElementaryUnit : BaseEntity
    {
        public Guid? TicketTemplateParagraphDataId { get; set; }

        public Guid? ParentElementaryUnitId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public int Order { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplateParagraphData TicketTemplateParagraphData { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("TicketTemplateElementaryUnitId")]
        public virtual List<TicketTemplateElementaryAttribute> TicketTemplateElementaryAttributes { get; set; }

        [ForeignKey("ParentElementaryUnitId")]
        public virtual List<TicketTemplateElementaryUnit> ChildElementaryUnits { get; set; }

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

            if (ChildElementaryUnits != null && ChildElementaryUnits.Count > 0)
            {
                StringBuilder nodes = new StringBuilder();
                foreach (var child in ChildElementaryUnits)
                {
                    nodes.Append(child?.ToString() ?? string.Empty);
                }

                return string.Format("<{0}{1}>{2}</{0}>", Name ?? string.Empty, attributes.ToString(), nodes.ToString());
            }
            else
            {
                return string.Format("<{0}{1}/>", Name ?? string.Empty, attributes.ToString());
            }
        }
    }
}