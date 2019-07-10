using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Examination
{
    /// <summary>
    /// Класс, описывает колонку таблицы
    /// </summary>
    [DataContract]
    public class TicketTemplateTableGridColumn : IdEntity
    {
        [DataMember]
        [ForeignKey("TicketTemplateTable")]
        public Guid TicketTemplateTableId { get; set; }

        [DataMember]
        public int Order { get; set; }

        [DataMember]
        public string Width { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplateTable TicketTemplateTable { get; set; }

        //-------------------------------------------------------------------------
    }
}