using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Examination
{
    /// <summary>
    /// Класс, описывающий строку
    /// </summary>
    [DataContract]
    public class TicketTemplateTableRow : IdEntity
    {
        [DataMember]
        public Guid? TicketTemplateTableId { get; set; }

        [DataMember]
        public Guid? TicketTemplateTableRowPropertiesId { get; set; }

        [DataMember]
        public int Order { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplateTable TicketTemplateTable { get; set; }

        public virtual TicketTemplateTableRowProperties TicketTemplateTableRowProperties { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("TicketTemplateTableRowId")]
        public virtual List<TicketTemplateTableCell> TicketTemplateTableCells { get; set; }

        //-------------------------------------------------------------------------
    }
}