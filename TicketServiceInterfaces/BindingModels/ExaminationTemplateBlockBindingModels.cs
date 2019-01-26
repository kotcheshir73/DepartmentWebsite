using DepartmentService.BindingModels;
using System;

namespace TicketServiceInterfaces.BindingModels
{
    public class ExaminationTemplateBlockGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? ExaminationTemplateId { get; set; }
    }

    public class ExaminationTemplateBlockSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid ExaminationTemplateId { get; set; }

        public string BlockName { get; set; }

        public string QuestionTagInTemplate { get; set; }

        public int CountQuestionInTicket { get; set; }
    }
}