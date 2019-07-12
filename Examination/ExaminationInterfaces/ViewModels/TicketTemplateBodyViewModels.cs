using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class TicketTemplateBodyPageViewModel : PageSettingListViewModel<TicketTemplateBodyViewModel> { }

    public class TicketTemplateBodyViewModel : PageSettingElementViewModel
    {
        public Guid TicketTemplateId { get; set; }

        public Guid? TicketTemplateBodyPropertiesId { get; set; }

        public TicketTemplateBodyPropertiesViewModel TicketTemplateBodyPropertiesViewModel { get; set; }

        public TicketTemplateTablePageViewModel TicketTemplateTablePageViewModel { get; set; }

        public TicketTemplateParagraphPageViewModel TicketTemplateParagraphPageViewModel { get; set; }
    }
}