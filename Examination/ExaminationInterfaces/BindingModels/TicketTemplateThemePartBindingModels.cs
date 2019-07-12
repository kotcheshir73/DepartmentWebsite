using System;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class TicketTemplateThemePartGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? TicketTemplateId { get; set; }
    }

    public class TicketTemplateThemePartSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid TicketTemplateId { get; set; }

        public string InnerXml { get; set; }

        public int Order { get; set; }
    }
}