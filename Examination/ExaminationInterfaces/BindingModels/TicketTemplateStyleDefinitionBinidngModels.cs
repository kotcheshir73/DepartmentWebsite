using System;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class TicketTemplateStyleDefinitionGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? TicketTemplateId { get; set; }
    }

    public class TicketTemplateStyleDefinitionSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid TicketTemplateId { get; set; }

        public string InnerXml { get; set; }

        public int Order { get; set; }
    }
}