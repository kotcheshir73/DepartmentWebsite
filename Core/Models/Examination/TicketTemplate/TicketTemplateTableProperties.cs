using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Examination
{
    /// <summary>
    /// Класс, описывает свойства таблицы
    /// </summary>
    [DataContract]
    public class TicketTemplateTableProperties : IdEntity
    {
        [DataMember]
        [ForeignKey("TicketTemplateTable")]
        public Guid? TicketTemplateTableId { get; set; }

        [DataMember]
        public string Width { get; set; }

        [DataMember]
        public string LookValue { get; set; }

        [DataMember]
        public string LookFirstRow { get; set; }

        [DataMember]
        public string LookLastRow { get; set; }

        [DataMember]
        public string LookFirstColumn { get; set; }

        [DataMember]
        public string LookLastColumn { get; set; }

        [DataMember]
        public string LookNoHorizontalBand { get; set; }

        [DataMember]
        public string LookNoVerticalBand { get; set; }

        [DataMember]
        public string LayoutType { get; set; }

        [DataMember]
        public string BorderTopValue { get; set; }

        [DataMember]
        public string BorderTopColor { get; set; }

        [DataMember]
        public string BorderTopSize { get; set; }

        [DataMember]
        public string BorderTopSpace { get; set; }

        [DataMember]
        public string BorderBottomValue { get; set; }

        [DataMember]
        public string BorderBottomColor { get; set; }

        [DataMember]
        public string BorderBottomSize { get; set; }

        [DataMember]
        public string BorderBottomSpace { get; set; }

        [DataMember]
        public string BorderLeftValue { get; set; }

        [DataMember]
        public string BorderLeftColor { get; set; }

        [DataMember]
        public string BorderLeftSize { get; set; }

        [DataMember]
        public string BorderLeftSpace { get; set; }

        [DataMember]
        public string BorderRightValue { get; set; }

        [DataMember]
        public string BorderRightColor { get; set; }

        [DataMember]
        public string BorderRightSize { get; set; }

        [DataMember]
        public string BorderRightSpace { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplateTable TicketTemplateTable { get; set; }

        //-------------------------------------------------------------------------
    }
}