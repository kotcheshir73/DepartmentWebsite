using DepartmentService.ViewModels;
using System;

namespace TicketServiceInterfaces.ViewModels
{
    public class ExaminationTemplatePageViewModel : PageViewModel<ExaminationTemplateViewModel> { }

    public class ExaminationTemplateViewModel
    {
        public Guid Id { get; set; }

        public Guid? EducationDirectionId { get; set; }

        public Guid DisciplineId { get; set; }

        public Guid? TicketTemplateId { get; set; }

        public string EducationDirectionName { get; set; }

        public string Disciplne { get; set; }

        public string Semester { get; set; }

        public string TicketTemplateName { get; set; }
    }
}