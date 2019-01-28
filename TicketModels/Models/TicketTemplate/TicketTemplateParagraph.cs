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
    }
}