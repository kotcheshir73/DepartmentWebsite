using DepartmentService.BindingModels;
using System;

namespace TicketServiceInterfaces.BindingModels
{
    public class TicketTemplateGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? ExaminationTemplateId { get; set; }
    }

    public class TicketTemplateSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid? ExaminationTemplateId { get; set; }

        public string TemplateName { get; set; }
    }
}