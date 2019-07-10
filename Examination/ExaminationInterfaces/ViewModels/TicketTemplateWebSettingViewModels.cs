using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class TicketTemplateWebSettingPageViewModel : PageSettingListViewModel<TicketTemplateWebSettingViewModel> { }

    public class TicketTemplateWebSettingViewModel : PageSettingElementViewModel
    {
        public Guid? TicketTemplateId { get; set; }

        public string InnerXml { get; set; }

        public int Order { get; set; }
    }
}