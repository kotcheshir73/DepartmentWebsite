using DepartmentService.ViewModels;
using System;

namespace TicketServiceInterfaces.ViewModels
{
    public class ExaminationTemplateTicketPageViewModel : PageViewModel<ExaminationTemplateTicketViewModel> { }

    public class ExaminationTemplateTicketViewModel
    {
        public Guid Id { get; set; }

        public Guid ExaminationTemplateId { get; set; }

        public string ExaminationTemplateName { get; set; }

        public int TicketNumber { get; set; }
    }
}