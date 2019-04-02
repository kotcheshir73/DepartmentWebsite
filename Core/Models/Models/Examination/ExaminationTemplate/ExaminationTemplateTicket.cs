using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Examination
{
    /// <summary>
    /// Класс, описывающий билет экзамена
    /// </summary>
    [DataContract]
    public class ExaminationTemplateTicket : BaseEntity
    {
        [DataMember]
        public Guid ExaminationTemplateId { get; set; }

        [DataMember]
        public int TicketNumber { get; set; }

        //-------------------------------------------------------------------------

        public virtual ExaminationTemplate ExaminationTemplate { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("ExaminationTemplateTicketId")]
        public virtual List<ExaminationTemplateTicketQuestion> ExaminationTemplateTicketQuestions { get; set; }
    }
}