using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class TicketTemplateBodyPropertiesPageViewModel : PageSettingListViewModel<TicketTemplateBodyPropertiesViewModel> { }

    public class TicketTemplateBodyPropertiesViewModel : PageSettingElementViewModel
    {
        public Guid? TicketTemplateBodyId { get; set; }
        
        public string PageSizeHeight { get; set; }
        
        public string PageSizeWidth { get; set; }
        
        public string PageSizeOrient { get; set; }
        
        public string PageMarginBottom { get; set; }
        
        public string PageMarginTop { get; set; }
        
        public string PageMarginLeft { get; set; }
        
        public string PageMarginRight { get; set; }
        
        public string PageMarginFooter { get; set; }
        
        public string PageMarginGutter { get; set; }
    }
}