using DepartmentModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TicketModels.Models
{
    public class TicketTemplateBody : BaseEntity
    {
        public Guid TicketTemplateId { get; set; }

        public Guid? BodyFormatId { get; set; }

        public string BodyName { get; set; }

        public string SectName { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplate TicketTemplate { get; set; }

        public virtual TicketTemplateElementaryUnit BodyFormat { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("TicketTemplateBodyId")]
        public virtual List<TicketTemplateParagraph> TicketTemplateParagraphs { get; set; }

        [ForeignKey("TicketTemplateBodyId")]
        public virtual List<TicketTemplateTable> TicketTemplateTables { get; set; }

        //-------------------------------------------------------------------------

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            var paragraphs = TicketTemplateParagraphs?.OrderBy(x => x.Order).ToList() ?? new List<TicketTemplateParagraph>();
            var tables = TicketTemplateTables?.OrderBy(x => x.Order).ToList() ?? new List<TicketTemplateTable>();

            for (int i = 0; i < paragraphs.Count + tables.Count; ++i)
            {
                var paragraph = paragraphs.FirstOrDefault(x => x.Order == i);
                if (paragraph != null)
                {
                    sb.Append(paragraph.ToString());
                }
                var table = tables.FirstOrDefault(x => x.Order == i);
                if (table != null)
                {
                    sb.Append(table.ToString());
                }
            }

            return string.Format("<{0}><{1}>{2}{3}</{1}></{0}>", BodyName ?? string.Empty, SectName ?? string.Empty, sb.ToString(), BodyFormat?.ToString() ?? string.Empty);
        }
    }
}