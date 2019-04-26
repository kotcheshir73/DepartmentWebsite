using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class ExaminationTemplateTicketPageViewModel : PageSettingListViewModel<ExaminationTemplateTicketViewModel> { }

    public class ExaminationTemplateTicketViewModel : PageSettingElementViewModel
    {
        public Guid ExaminationTemplateId { get; set; }

        public string ExaminationTemplateName { get; set; }

        public int TicketNumber { get; set; }
    }
}