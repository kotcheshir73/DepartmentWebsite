using System;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class TicketTemplateGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? ExaminationTemplateId { get; set; }
    }

    public class TicketTemplateSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid? ExaminationTemplateId { get; set; }

        public string TemplateName { get; set; }
    }
}