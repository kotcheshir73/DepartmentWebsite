using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class TicketTemplateParagraphRunPageViewModel : PageSettingListViewModel<TicketTemplateParagraphRunViewModel> { }

    public class TicketTemplateParagraphRunViewModel : PageSettingElementViewModel
    {
        public Guid TicketTemplateParagraphId { get; set; }
        
        public Guid? TicketTemplateRunPropertiesId { get; set; }

        public TicketTemplateParagraphRunPropertiesViewModel TicketTemplateParagraphRunPropertiesViewModel { get; set; }

        public string Text { get; set; }

        public int Order { get; set; }
    }
}