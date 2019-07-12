using System;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class TicketTemplateNumberingGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? TicketTemplateId { get; set; }
    }

    public class TicketTemplateNumberingSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid TicketTemplateId { get; set; }

        public string InnerXml { get; set; }

        public int Order { get; set; }
    }
}