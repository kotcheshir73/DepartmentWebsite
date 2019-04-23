using DepartmentService.BindingModels;
using System;

namespace TicketServiceInterfaces.BindingModels
{
    public class ExaminationTemplateTicketQuestionGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? ExaminationTemplateTicketId { get; set; }
    }

    public class ExaminationTemplateTicketQuestionSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid ExaminationTemplateTicketId { get; set; }

        public Guid ExaminationTemplateBlockQuestionId { get; set; }

        public Guid ExaminationTemplateBlockId { get; set; }

        public int Order { get; set; }
    }
}