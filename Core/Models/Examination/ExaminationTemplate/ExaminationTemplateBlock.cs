using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Examination
{
    /// <summary>
    /// Класс, описывающий блок экзамена
    /// </summary>
    [DataContract]
    public class ExaminationTemplateBlock : BaseEntity
    {
        [DataMember]
        public Guid ExaminationTemplateId { get; set; }

        [DataMember]
        public string BlockName { get; set; }

        [DataMember]
        public string QuestionTagInTemplate { get; set; }

        [DataMember]
        public int CountQuestionInTicket { get; set; }

        [DataMember]
        public bool IsCombine { get; set; }

        [DataMember]
        public string CombineBlocks { get; set; }

        //-------------------------------------------------------------------------

        public virtual ExaminationTemplate ExaminationTemplate { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("ExaminationTemplateBlockId")]
        public virtual List<ExaminationTemplateBlockQuestion> ExaminationTemplateBlockQuestions { get; set; }
    }
}