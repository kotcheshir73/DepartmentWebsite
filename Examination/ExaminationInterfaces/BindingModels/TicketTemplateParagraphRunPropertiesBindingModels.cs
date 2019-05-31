using System;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class TicketTemplateParagraphRunPropertiesGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? TicketTemplateParagraphRunId { get; set; }
    }

    public class TicketTemplateParagraphRunPropertiesSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid TicketTemplateParagraphRunId { get; set; }
        
        public bool RunBold { get; set; }
        
        public bool RunItalic { get; set; }
        
        public bool RunUnderline { get; set; }
        
        public string RunSize { get; set; }
    }
}