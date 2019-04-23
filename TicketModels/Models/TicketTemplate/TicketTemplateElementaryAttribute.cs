using DepartmentModel.Models;
using System;

namespace TicketModels.Models
{
    public class TicketTemplateElementaryAttribute : BaseEntity
    {
        public Guid? TicketTemplateParagraphDataId { get; set; }

        public Guid? TicketTemplateParagraphId { get; set; }

        public Guid? TicketTemplateTableRowId { get; set; }

        public Guid? TicketTemplateElementaryUnitId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplateParagraph TicketTemplateParagraph { get; set; }

        public virtual TicketTemplateParagraphData TicketTemplateParagraphData { get; set; }

        public virtual TicketTemplateTableRow TicketTemplateTableRow { get; set; }

        public virtual TicketTemplateElementaryUnit TicketTemplateElementaryUnit { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------
    }
}