using System;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class TicketTemplateTableCellPropertiesGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid TicketTemplateTableCellId { get; set; }
    }

    public class TicketTemplateTableCellPropertiesSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid TicketTemplateTableCellId { get; set; }

        public string TableCellWidth { get; set; }
        
        public string GridSpan { get; set; }
        
        public string VerticalMerge { get; set; }
        
        public string ShadingValue { get; set; }
        
        public string ShadingColor { get; set; }
        
        public string ShadingFill { get; set; }
    }
}