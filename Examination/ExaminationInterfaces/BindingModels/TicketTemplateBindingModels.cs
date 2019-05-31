using System;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class TicketTemplateGetBindingModel : PageSettingGetBinidingModel { }

    public class TicketTemplateSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid? TicketTemplateBodyId { get; set; }

        public string TemplateName { get; set; }

        public TicketTemplateBodySetBindingModel TicketTemplateBodySetBindingModel { get; set; }
    }
}