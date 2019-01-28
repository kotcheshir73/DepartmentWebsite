using DepartmentModel.Models;
using System;
using System.Runtime.Serialization;

namespace TicketModels.Models
{
    /// <summary>
    /// Класс, описывающий вопрос билета экзамена
    /// </summary>
    [DataContract]
    public class ExaminationTemplateTicketQuestion : BaseEntity
    {
        [DataMember]
        public Guid ExaminationTemplateTicketId { get; set; }

        [DataMember]
        public Guid ExaminationTemplateBlockQuestionId { get; set; }

        [DataMember]
        public Guid ExaminationTemplateBlockId { get; set; }

        [DataMember]
        public int Order { get; set; }

        //-------------------------------------------------------------------------

        public virtual ExaminationTemplateTicket ExaminationTemplateTicket { get; set; }

        public virtual ExaminationTemplateBlockQuestion ExaminationTemplateBlockQuestion { get; set; }

        //-------------------------------------------------------------------------
    }
}