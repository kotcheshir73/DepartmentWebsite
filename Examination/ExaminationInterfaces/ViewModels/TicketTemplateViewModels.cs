using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class TicketTemplatePageViewModel : PageSettingListViewModel<TicketTemplateViewModel> { }

    public class TicketTemplateViewModel : PageSettingElementViewModel
    {
        public Guid? ExaminationTemplateId { get; set; }

        public string TemplateName { get; set; }

        public TicketTemplateBodyViewModel Body { get; set; }
    }
}