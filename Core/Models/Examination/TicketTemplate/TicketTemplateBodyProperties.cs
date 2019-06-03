using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Examination
{
    /// <summary>
    /// Класс отвечает за свойства документа
    /// </summary>
    [DataContract]
    public class TicketTemplateBodyProperties : IdEntity
    {
        [DataMember]
        [ForeignKey("TicketTemplateBody")]
        public Guid? TicketTemplateBodyId { get; set; }

        [DataMember]
        public string PageSizeHeight { get; set; }

        [DataMember]
        public string PageSizeWidth { get; set; }

        [DataMember]
        public string PageSizeOrient { get; set; }

        [DataMember]
        public string PageMarginBottom { get; set; }

        [DataMember]
        public string PageMarginTop { get; set; }

        [DataMember]
        public string PageMarginLeft { get; set; }

        [DataMember]
        public string PageMarginRight { get; set; }

        [DataMember]
        public string PageMarginFooter { get; set; }

        [DataMember]
        public string PageMarginGutter { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplateBody TicketTemplateBody { get; set; }

        //-------------------------------------------------------------------------
    }
}
