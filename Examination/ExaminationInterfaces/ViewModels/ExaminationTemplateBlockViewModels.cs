using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class ExaminationTemplateBlockPageViewModel : PageSettingListViewModel<ExaminationTemplateBlockViewModel> { }

    public class ExaminationTemplateBlockViewModel : PageSettingElementViewModel
    {
        public Guid ExaminationTemplateId { get; set; }

        public string ExaminationTemplateName { get; set; }

        public string BlockName { get; set; }

        public string QuestionTagInTemplate { get; set; }

        public int CountQuestionInTicket { get; set; }

        public bool IsCombine { get; set; }

        public string CombineBlocks { get; set; }
    }
}