using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class TicketTemplateParagraphPropertiesPageViewModel : PageSettingListViewModel<TicketTemplateParagraphPropertiesViewModel> { }

    public class TicketTemplateParagraphPropertiesViewModel : PageSettingElementViewModel
    {
        public Guid TicketTemplateParagraphId { get; set; }
        
        public string Justification { get; set; }
        
        public string SpacingBetweenLinesLine { get; set; }
        
        public string SpacingBetweenLinesLineRule { get; set; }
        
        public string SpacingBetweenLinesBefore { get; set; }
        
        public string SpacingBetweenLinesAfter { get; set; }
        
        public string IndentationFirstLine { get; set; }
        
        public string IndentationHanging { get; set; }
        
        public string IndentationLeft { get; set; }
        
        public string IndentationRight { get; set; }
        
        public bool RunBold { get; set; }
        
        public bool RunItalic { get; set; }
        
        public bool RunUnderline { get; set; }
        
        public string RunSize { get; set; }
    }
}