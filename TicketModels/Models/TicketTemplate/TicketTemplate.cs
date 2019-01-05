using DepartmentModel.Models;
using System;

namespace TicketModels.Models
{
    public class TicketTemplate : BaseEntity
    {
        public string XML { get; set; }

        public string TemplateName { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplateBody TicketTemplateBody { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------
    }
}