using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Examination
{
    /// <summary>
    /// Класс, описывающий свойства ячейки
    /// </summary>
    [DataContract]
    public class TicketTemplateTableCellProperties : IdEntity
    {
        [DataMember]
        [ForeignKey("TicketTemplateTableCell")]
        public Guid? TicketTemplateTableCellId { get; set; }

        [DataMember]
        public string TableCellWidth { get; set; }

        [DataMember]
        public string GridSpan { get; set; }

        [DataMember]
        public string VerticalMerge { get; set; }

        [DataMember]
        public string ShadingValue { get; set; }

        [DataMember]
        public string ShadingColor { get; set; }

        [DataMember]
        public string ShadingFill { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplateTableCell TicketTemplateTableCell { get; set; }

        //-------------------------------------------------------------------------
    }
}