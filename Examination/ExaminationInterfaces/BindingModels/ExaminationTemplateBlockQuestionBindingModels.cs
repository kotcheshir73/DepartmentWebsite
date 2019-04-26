using System;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class ExaminationTemplateBlockQuestionGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? ExaminationTemplateBlockId { get; set; }

        public Guid? ExaminationTemplateId { get; set; }
    }

    public class ExaminationTemplateBlockQuestionSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid ExaminationTemplateBlockId { get; set; }

        public int QuestionNumber { get; set; }

        public string QuestionText { get; set; }

        public byte[] QuestionImage { get; set; }
    }
}