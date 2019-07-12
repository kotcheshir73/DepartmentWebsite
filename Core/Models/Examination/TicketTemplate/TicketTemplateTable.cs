using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Examination
{
    /// <summary>
    /// Класс, описывает таблицу
    /// </summary>
    [DataContract]
    public class TicketTemplateTable : IdEntity
    {
        [DataMember]
        public Guid TicketTemplateBodyId { get; set; }

        [DataMember]
        public Guid? TicketTemplateTablePropertiesId { get; set; }

        [DataMember]
        public int Order { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplateBody TicketTemplateBody { get; set; }

        public virtual TicketTemplateTableProperties TicketTemplateTableProperties { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("TicketTemplateTableId")]
        public virtual List<TicketTemplateTableRow> TicketTemplateTableRows { get; set; }

        [ForeignKey("TicketTemplateTableId")]
        public virtual List<TicketTemplateTableGridColumn> TicketTemplateTableGridColumns { get; set; }

        //-------------------------------------------------------------------------
    }
}