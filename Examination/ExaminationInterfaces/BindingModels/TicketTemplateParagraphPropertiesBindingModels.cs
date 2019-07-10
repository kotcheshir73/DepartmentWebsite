using System;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class TicketTemplateParagraphPropertiesGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid TicketTemplateParagraphId { get; set; }
    }

    public class TicketTemplateParagraphPropertiesSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid TicketTemplateParagraphId { get; set; }
        
        public string NumberingLevelReference { get; set; }
        
        public string NumberingId { get; set; }

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