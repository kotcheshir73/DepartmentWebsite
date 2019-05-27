using System;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class TicketTemplateBodyPropertiesGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? TicketTemplateBodyId { get; set; }
    }

    public class TicketTemplateBodyPropertiesSetBindingModel : PageSettingSetBinidingModel
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