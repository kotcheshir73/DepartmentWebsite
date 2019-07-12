using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Examination
{
    /// <summary>
    /// Класс, описывающий ячейку
    /// </summary>
    [DataContract]
    public class TicketTemplateTableCell : IdEntity
    {
        [DataMember]
        public Guid TicketTemplateTableRowId { get; set; }

        [DataMember]
        public Guid? TicketTemplateTableCellPropertiesId { get; set; }

        [DataMember]
        public int Order { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplateTableRow TicketTemplateTableRow { get; set; }

        public virtual TicketTemplateTableCellProperties TicketTemplateTableCellProperties { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("TicketTemplateTableCellId")]
        public virtual List<TicketTemplateParagraph> TicketTemplateParagraphs { get; set; }

        //-------------------------------------------------------------------------
    }
}