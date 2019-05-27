using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class TicketTemplateParagraphRunPropertiesPageViewModel : PageSettingListViewModel<TicketTemplateParagraphRunPropertiesViewModel> { }

    public class TicketTemplateParagraphRunPropertiesViewModel : PageSettingElementViewModel
    {
        public Guid TicketTemplateParagraphRunId { get; set; }
        
        public bool RunBold { get; set; }
        
        public bool RunItalic { get; set; }
        
        public bool RunUnderline { get; set; }
        
        public string RunSize { get; set; }
    }
}