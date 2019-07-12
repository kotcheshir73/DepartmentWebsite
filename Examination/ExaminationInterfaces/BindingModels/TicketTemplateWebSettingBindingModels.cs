using System;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class TicketTemplateWebSettingGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? TicketTemplateId { get; set; }
    }

    public class TicketTemplateWebSettingSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid TicketTemplateId { get; set; }

        public string InnerXml { get; set; }

        public int Order { get; set; }
    }
}