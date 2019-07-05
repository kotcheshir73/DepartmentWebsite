using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Examination
{
    /// <summary>
    /// Класс, описывающий свойства строки
    /// </summary>
    [DataContract]
    public class TicketTemplateTableRowProperties : IdEntity
    {
        [DataMember]
        [ForeignKey("TicketTemplateTableRow")]
        public Guid? TicketTemplateTableRowId { get; set; }

        [DataMember]
        public string CantSplit { get; set; }

        [DataMember]
        public string TableRowHeight { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplateTableRow TicketTemplateTableRow { get; set; }

        //-------------------------------------------------------------------------
    }
}