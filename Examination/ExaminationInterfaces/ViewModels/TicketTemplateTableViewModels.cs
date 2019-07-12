using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class TicketTemplateTablePageViewModel : PageSettingListViewModel<TicketTemplateTableViewModel> { }

    public class TicketTemplateTableViewModel : PageSettingElementViewModel
    {
        public Guid TicketTemplateBodyId { get; set; }

        public Guid? TicketTemplateTablePropertiesId { get; set; }

        public TicketTemplateTablePropertiesViewModel TicketTemplateTablePropertiesViewModel { get; set; }

        public TicketTemplateTableRowPageViewModel TicketTemplateTableRowPageViewModel { get; set; }

        public TicketTemplateTableGridColumnPageViewModel TicketTemplateTableGridColumnPageViewModel { get; set; }

        public int Order { get; set; }
    }
}