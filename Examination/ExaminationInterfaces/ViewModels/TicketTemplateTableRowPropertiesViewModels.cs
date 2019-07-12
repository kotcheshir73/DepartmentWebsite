using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class TicketTemplateTableRowPropertiesPageViewModel : PageSettingListViewModel<TicketTemplateTableRowPropertiesViewModel> { }

    public class TicketTemplateTableRowPropertiesViewModel : PageSettingElementViewModel
    {
        public Guid? TicketTemplateTableRowId { get; set; }
        
        public string CantSplit { get; set; }
        
        public string TableRowHeight { get; set; }
    }
}