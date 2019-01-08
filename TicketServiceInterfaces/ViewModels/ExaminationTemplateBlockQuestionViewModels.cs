using DepartmentService.ViewModels;
using System;
using System.Drawing;

namespace TicketServiceInterfaces.ViewModels
{
    public class ExaminationTemplateBlockQuestionPageViewModel : PageViewModel<ExaminationTemplateBlockQuestionViewModel> { }

    public class ExaminationTemplateBlockQuestionViewModel
    {
        public Guid Id { get; set; }

        public Guid ExaminationTemplateBlockId { get; set; }

        public string ExaminationTemplateBlockName { get; set; }

        public int QuestionNumber { get; set; }

        public string QuestionText { get; set; }

        public Image QuestionImage { get; set; }
    }
}