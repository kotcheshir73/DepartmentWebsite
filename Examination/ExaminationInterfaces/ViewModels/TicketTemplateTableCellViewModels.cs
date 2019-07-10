using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class TicketTemplateTableCellPageViewModel : PageSettingListViewModel<TicketTemplateTableCellViewModel> { }

    public class TicketTemplateTableCellViewModel : PageSettingElementViewModel
    {
        public Guid TicketTemplateTableRowId { get; set; }
        
        public Guid? TicketTemplateTableCellPropertiesId { get; set; }

        public TicketTemplateTableCellPropertiesViewModel TicketTemplateTableCellPropertiesViewModel { get; set; }

        public TicketTemplateParagraphPageViewModel TicketTemplateParagraphPageViewModel { get; set; }

        public int Order { get; set; }
    }
}