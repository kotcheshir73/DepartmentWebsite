using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class TicketTemplateTableCellPropertiesPageViewModel : PageSettingListViewModel<TicketTemplateTableCellPropertiesViewModel> { }

    public class TicketTemplateTableCellPropertiesViewModel : PageSettingElementViewModel
    {
        public Guid? TicketTemplateTableCellId { get; set; }
        
        public string TableCellWidth { get; set; }
        
        public string GridSpan { get; set; }
        
        public string VerticalMerge { get; set; }
        
        public string ShadingValue { get; set; }
        
        public string ShadingColor { get; set; }
        
        public string ShadingFill { get; set; }
    }
}