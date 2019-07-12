using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class TicketTemplatePageViewModel : PageSettingListViewModel<TicketTemplateViewModel> { }

    public class TicketTemplateViewModel : PageSettingElementViewModel
    {
        public string TemplateName { get; set; }

        public TicketProcessBodyViewModel Body { get; set; }
    }
}