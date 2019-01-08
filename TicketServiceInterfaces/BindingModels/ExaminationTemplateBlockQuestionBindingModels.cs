using DepartmentService.BindingModels;
using System;

namespace TicketServiceInterfaces.BindingModels
{
    public class ExaminationTemplateBlockQuestionGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? ExaminationTemplateBlockId { get; set; }
    }

    public class ExaminationTemplateBlockQuestionSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid ExaminationTemplateBlockId { get; set; }

        public int QuestionNumber { get; set; }

        public string QuestionText { get; set; }

        public byte[] QuestionImage { get; set; }
    }
}