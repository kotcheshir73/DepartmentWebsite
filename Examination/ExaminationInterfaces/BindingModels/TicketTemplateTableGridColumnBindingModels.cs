using System;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class TicketTemplateTableGridColumnGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? TicketTemplateTableId { get; set; }
    }

    public class TicketTemplateTableGridColumnSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid? TicketTemplateTableId { get; set; }

        public string Width { get; set; }

        public int Order { get; set; }
    }
}