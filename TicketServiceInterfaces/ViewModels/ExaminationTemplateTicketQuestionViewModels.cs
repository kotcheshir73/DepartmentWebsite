using DepartmentService.ViewModels;
using System;

namespace TicketServiceInterfaces.ViewModels
{
    public class ExaminationTemplateTicketQuestionPageViewModel : PageViewModel<ExaminationTemplateTicketQuestionViewModel> { }

    public class ExaminationTemplateTicketQuestionViewModel
    {
        public Guid Id { get; set; }

        public Guid ExaminationTemplateTicketId { get; set; }

        public Guid ExaminationTemplateBlockQuestionId { get; set; }

        public Guid ExaminationTemplateBlockId { get; set; }

        public int ExaminationTemplateTicketNumber { get; set; }

        public int ExaminationTemplateBlockQuestionNumber { get; set; }

        public string ExaminationTemplateBlockQuestionQuestion { get; set; }

        public int Order { get; set; }
    }
}