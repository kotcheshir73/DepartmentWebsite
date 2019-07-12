using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class TicketTemplateFontTablePageViewModel : PageSettingListViewModel<TicketTemplateFontTableViewModel> { }

    public class TicketTemplateFontTableViewModel : PageSettingElementViewModel
    {
        public Guid? TicketTemplateId { get; set; }

        public string InnerXml { get; set; }

        public int Order { get; set; }
    }
}