using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class ExaminationTemplateTicketQuestionPageViewModel : PageSettingListViewModel<ExaminationTemplateTicketQuestionViewModel> { }

    public class ExaminationTemplateTicketQuestionViewModel : PageSettingElementViewModel
    {
        public Guid ExaminationTemplateTicketId { get; set; }

        public Guid ExaminationTemplateBlockQuestionId { get; set; }

        public Guid ExaminationTemplateBlockId { get; set; }

        public int ExaminationTemplateTicketNumber { get; set; }

        public int ExaminationTemplateBlockQuestionNumber { get; set; }

        public string ExaminationTemplateBlockQuestionQuestion { get; set; }

        public int Order { get; set; }
    }
}