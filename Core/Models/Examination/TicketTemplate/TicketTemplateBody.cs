using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Examination
{
    /// <summary>
    /// Класс описывает тело документа
    /// </summary>
    [DataContract]
    public class TicketTemplateBody : BaseEntity
    {
        [DataMember]
        [ForeignKey("TicketTemplate")]
        public Guid? TicketTemplateId { get; set; }

        [DataMember]
        public Guid? TicketTemplateBodyPropertiesId { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplate TicketTemplate { get; set; }

        public virtual TicketTemplateBodyProperties TicketTemplateBodyProperties { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("TicketTemplateBodyId")]
        public virtual List<TicketTemplateParagraph> TicketTemplateParagraphs { get; set; }

        [ForeignKey("TicketTemplateBodyId")]
        public virtual List<TicketTemplateTable> TicketTemplateTables { get; set; }

        //-------------------------------------------------------------------------
    }
}