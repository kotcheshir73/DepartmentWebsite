using System;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class ExaminationTemplateTicketGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? ExaminationTemplateId { get; set; }
    }

    public class ExaminationTemplateTicketSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid ExaminationTemplateId { get; set; }

        public int TicketNumber { get; set; }
    }
}