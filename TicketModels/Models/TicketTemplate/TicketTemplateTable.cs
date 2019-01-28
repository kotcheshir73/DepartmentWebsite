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
    }
}