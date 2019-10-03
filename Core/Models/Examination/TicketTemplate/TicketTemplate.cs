using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Examination
{
    [DataContract]
    public class TicketTemplate : IdEntity
    {
        [DataMember]
        public string TemplateName { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------

        [ForeignKey("TicketTemplateId")]
        public virtual List<TicketTemplateBody> TicketTemplateBodies { get; set; }

        [ForeignKey("TicketTemplateId")]
        public virtual List<ExaminationTemplate> ExaminationTemplates { get; set; }

        [ForeignKey("TicketTemplateId")]
        public virtual List<TicketTemplateFontTable> TicketTemplateFontTables { get; set; }

        [ForeignKey("TicketTemplateId")]
        public virtual List<TicketTemplateNumbering> TicketTemplateNumberings { get; set; }

        [ForeignKey("TicketTemplateId")]
        public virtual List<TicketTemplateDocumentSetting> TicketTemplateDocumentSettings { get; set; }

        [ForeignKey("TicketTemplateId")]
        public virtual List<TicketTemplateStyleDefinition> TicketTemplateStyleDefinitions { get; set; }

        [ForeignKey("TicketTemplateId")]
        public virtual List<TicketTemplateWebSetting> TicketTemplateWebSettings { get; set; }

        [ForeignKey("TicketTemplateId")]
        public virtual List<TicketTemplateThemePart> TicketTemplateThemeParts { get; set; }

        //-------------------------------------------------------------------------
    }
}