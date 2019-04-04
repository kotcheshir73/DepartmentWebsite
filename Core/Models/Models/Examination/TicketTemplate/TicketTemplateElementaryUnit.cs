using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Examination
{
    public class TicketTemplateElementaryUnit : BaseEntity
    {
        public Guid? TicketTemplateParagraphDataId { get; set; }

        public Guid? ParentElementaryUnitId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public int Order { get; set; }

        //-------------------------------------------------------------------------

            //TODO not link
       // public virtual TicketTemplateParagraphData TicketTemplateParagraphData { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("TicketTemplateElementaryUnitId")]
        public virtual List<TicketTemplateElementaryAttribute> TicketTemplateElementaryAttributes { get; set; }

        [ForeignKey("ParentElementaryUnitId")]
        public virtual List<TicketTemplateElementaryUnit> ChildElementaryUnits { get; set; }

        //-------------------------------------------------------------------------
    }
}