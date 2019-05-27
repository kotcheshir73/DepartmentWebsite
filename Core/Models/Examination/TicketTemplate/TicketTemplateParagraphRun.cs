﻿using System;
using System.Runtime.Serialization;

namespace Models.Examination
{
    /// <summary>
    /// Строка в документе
    /// </summary>
    [DataContract]
    public class TicketTemplateParagraphRun : BaseEntity
    {
        [DataMember]
        public Guid TicketTemplateParagraphId { get; set; }

        [DataMember]
        public Guid? TicketTemplateParagraphRunPropertiesId { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public int Order { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplateParagraph TicketTemplateParagraph { get; set; }

        public virtual TicketTemplateParagraphRunProperties TicketTemplateParagraphRunProperties { get; set; }

        //-------------------------------------------------------------------------
    }
}