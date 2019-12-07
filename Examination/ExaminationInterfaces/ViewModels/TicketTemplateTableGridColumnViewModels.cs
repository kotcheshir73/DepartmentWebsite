using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class TicketTemplateTableGridColumnPageViewModel : PageSettingListViewModel<TicketTemplateTableGridColumnViewModel> { }

    public class TicketTemplateTableGridColumnViewModel : PageSettingElementViewModel
    {
        public Guid TicketTemplateTableId { get; set; }
        
        public int Order { get; set; }
        
        public string Width { get; set; }
    }
}