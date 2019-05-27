using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class TicketTemplateParagraphPageViewModel : PageSettingListViewModel<TicketTemplateParagraphViewModel> { }

    public class TicketTemplateParagraphViewModel : PageSettingElementViewModel
    {
        public Guid? TicketTemplateBodyId { get; set; }
        
        public Guid? TicketTemplateTableCellId { get; set; }
        
        public Guid? TicketTemplateParagraphPropertiesId { get; set; }

        public TicketTemplateParagraphPropertiesViewModel TicketTemplateParagraphPropertiesViewModel { get; set; }

        public TicketTemplateParagraphRunPageViewModel TicketTemplateParagraphRunPageViewModel { get; set; }

        public int Order { get; set; }
    }
}