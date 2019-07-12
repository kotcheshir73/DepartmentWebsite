using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class TicketTemplateTableRowPageViewModel : PageSettingListViewModel<TicketTemplateTableRowViewModel> { }

    public class TicketTemplateTableRowViewModel : PageSettingElementViewModel
    {
        public Guid TicketTemplateTableId { get; set; }
        
        public Guid? TicketTemplateTableRowPropertiesId { get; set; }

        public TicketTemplateTableRowPropertiesViewModel TicketTemplateTableRowPropertiesViewModel { get; set; }

        public TicketTemplateTableCellPageViewModel TicketTemplateTableCellPageViewModel { get; set; }

        public int Order { get; set; }
    }
}