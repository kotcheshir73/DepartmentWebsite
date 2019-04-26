using Enums;
using System;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class ExaminationTemplateGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? DisciplineId { get; set; }
    }

    public class ExaminationTemplateSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid DisciplineId { get; set; }

        public Guid? EducationDirectionId { get; set; }

        public Semesters? Semester { get; set; }

        public string ExaminationTemplateName { get; set; }
    }
}