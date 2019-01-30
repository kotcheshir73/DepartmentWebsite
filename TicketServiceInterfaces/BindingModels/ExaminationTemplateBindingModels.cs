using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using System;

namespace TicketServiceInterfaces.BindingModels
{
    public class ExaminationTemplateGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? DisciplineId { get; set; }
    }

    public class ExaminationTemplateSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid DisciplineId { get; set; }

        public Guid? EducationDirectionId { get; set; }

        public Semesters? Semester { get; set; }

        public string ExaminationTemplateName { get; set; }
    }
}