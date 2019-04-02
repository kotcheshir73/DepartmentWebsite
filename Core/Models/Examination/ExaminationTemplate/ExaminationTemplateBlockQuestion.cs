using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Examination
{
    /// <summary>
    /// Класс, описывающий вопрос блока экзамена
    /// </summary>
    [DataContract]
    public class ExaminationTemplateBlockQuestion : BaseEntity
    {
        [DataMember]
        public Guid ExaminationTemplateBlockId { get; set; }

        [DataMember]
        public int QuestionNumber { get; set; }

        [DataMember]
        public string QuestionText { get; set; }

        [DataMember]
        public byte[] QuestionImage { get; set; }

        //-------------------------------------------------------------------------

        public virtual ExaminationTemplateBlock ExaminationTemplateBlock { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("ExaminationTemplateBlockQuestionId")]
        public virtual List<ExaminationTemplateTicketQuestion> ExaminationTemplateTicketQuestions { get; set; }
    }
}