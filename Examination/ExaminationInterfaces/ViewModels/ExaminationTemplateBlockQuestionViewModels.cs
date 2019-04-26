using System;
using System.Drawing;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class ExaminationTemplateBlockQuestionPageViewModel : PageSettingListViewModel<ExaminationTemplateBlockQuestionViewModel> { }

    public class ExaminationTemplateBlockQuestionViewModel : PageSettingElementViewModel
    {
        public Guid ExaminationTemplateBlockId { get; set; }

        public string ExaminationTemplateBlockName { get; set; }

        public int QuestionNumber { get; set; }

        public string QuestionText { get; set; }

        public Image QuestionImage { get; set; }
    }
}