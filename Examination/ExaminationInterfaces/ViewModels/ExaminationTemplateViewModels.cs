using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class ExaminationTemplatePageViewModel : PageSettingListViewModel<ExaminationTemplateViewModel> { }

    public class ExaminationTemplateViewModel : PageSettingElementViewModel
    {
        public Guid? EducationDirectionId { get; set; }

        public Guid? TicketTemplateId { get; set; }

        public Guid DisciplineId { get; set; }

        public string EducationDirectionName { get; set; }

        public string TicketTemplateName { get; set; }

        public string DisciplneName { get; set; }

        public string Semester { get; set; }

        public string ExaminationTemplateName { get; set; }
    }
}