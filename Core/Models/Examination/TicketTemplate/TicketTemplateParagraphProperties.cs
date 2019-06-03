using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Examination
{
    /// <summary>
    /// Свойства абзаца, которые нас могут инетерсовать
    /// </summary>
    [DataContract]
    public class TicketTemplateParagraphProperties : IdEntity
    {
        [DataMember]
        [ForeignKey("TicketTemplateParagraph")]
        public Guid TicketTemplateParagraphId { get; set; }

        [DataMember]
        public string Justification { get; set; }

        [DataMember]
        public string SpacingBetweenLinesLine { get; set; }

        [DataMember]
        public string SpacingBetweenLinesLineRule { get; set; }

        [DataMember]
        public string SpacingBetweenLinesBefore { get; set; }

        [DataMember]
        public string SpacingBetweenLinesAfter { get; set; }

        [DataMember]
        public string IndentationFirstLine { get; set; }

        [DataMember]
        public string IndentationHanging { get; set; }

        [DataMember]
        public string IndentationLeft { get; set; }

        [DataMember]
        public string IndentationRight { get; set; }

        [DataMember]
        public bool RunBold { get; set; }

        [DataMember]
        public bool RunItalic { get; set; }

        [DataMember]
        public bool RunUnderline { get; set; }

        [DataMember]
        public string RunSize { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplateParagraph TicketTemplateParagraph { get; set; }

        //-------------------------------------------------------------------------
    }
}