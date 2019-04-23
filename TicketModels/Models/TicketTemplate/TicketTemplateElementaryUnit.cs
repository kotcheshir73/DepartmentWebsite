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
    }
}