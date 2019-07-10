using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class TicketTemplateStyleDefinitionPageViewModel : PageSettingListViewModel<TicketTemplateStyleDefinitionViewModel> { }

    public class TicketTemplateStyleDefinitionViewModel : PageSettingElementViewModel
    {
        public Guid? TicketTemplateId { get; set; }

        public string InnerXml { get; set; }

        public int Order { get; set; }
    }
}