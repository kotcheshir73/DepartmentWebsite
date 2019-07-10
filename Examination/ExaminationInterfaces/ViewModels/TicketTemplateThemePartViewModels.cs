using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class TicketTemplateThemePartPageViewModel : PageSettingListViewModel<TicketTemplateThemePartViewModel> { }

    public class TicketTemplateThemePartViewModel : PageSettingElementViewModel
    {
        public Guid? TicketTemplateId { get; set; }

        public string InnerXml { get; set; }

        public int Order { get; set; }
    }
}