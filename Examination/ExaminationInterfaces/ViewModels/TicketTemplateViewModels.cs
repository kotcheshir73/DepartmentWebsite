using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class TicketTemplatePageViewModel : PageSettingListViewModel<TicketTemplateViewModel> { }

    public class TicketTemplateViewModel : PageSettingElementViewModel
    {
        public string TemplateName { get; set; }

        public TicketTemplateBodyViewModel Body { get; set; }

        public TicketTemplateDocumentSettingPageViewModel TicketTemplateDocumentSettingPageViewModel { get; set; }

        public TicketTemplateFontTablePageViewModel TicketTemplateFontTablePageViewModel { get; set; }

        public TicketTemplateNumberingPageViewModel TicketTemplateNumberingPageViewModel { get; set; }

        public TicketTemplateStyleDefinitionPageViewModel TicketTemplateStyleDefinitionPageViewModel { get; set; }

        public TicketTemplateThemePartPageViewModel TicketTemplateThemePartPageViewModel { get; set; }

        public TicketTemplateWebSettingPageViewModel TicketTemplateWebSettingPageViewModel { get; set; }
    }
}