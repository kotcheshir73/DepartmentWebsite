using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Examination
{
    /// <summary>
    /// Свойства строки
    /// </summary>
    [DataContract]
    public class TicketTemplateParagraphRunProperties : BaseEntity
    {
        [DataMember]
        [ForeignKey("TicketTemplateParagraphRun")]
        public Guid TicketTemplateParagraphRunId { get; set; }

        [DataMember]
        public bool RunBold { get; set; }

        [DataMember]
        public bool RunItalic { get; set; }

        [DataMember]
        public bool RunUnderline { get; set; }

        [DataMember]
        public string RunSize { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplateParagraphRun TicketTemplateParagraphRun { get; set; }

        //-------------------------------------------------------------------------
    }
}