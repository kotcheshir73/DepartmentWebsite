using System;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class TicketTemplateTableRowPropertiesGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid TicketTemplateTableRowId { get; set; }
    }

    public class TicketTemplateTableRowPropertiesSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid TicketTemplateTableRowId { get; set; }

        public string CantSplit { get; set; }
        
        public string TableRowHeight { get; set; }
    }
}