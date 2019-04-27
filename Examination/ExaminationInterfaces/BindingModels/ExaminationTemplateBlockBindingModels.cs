using System;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class ExaminationTemplateBlockGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? ExaminationTemplateId { get; set; }
    }

    public class ExaminationTemplateBlockSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid ExaminationTemplateId { get; set; }

        public string BlockName { get; set; }

        public string QuestionTagInTemplate { get; set; }

        public int CountQuestionInTicket { get; set; }

        public bool IsCombine { get; set; }

        public string CombineBlocks { get; set; }
    }
}