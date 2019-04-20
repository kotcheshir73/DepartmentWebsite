using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace LearningProgressInterfaces.BindingModels
{
    public class DisciplineLessonConductedGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? DisciplineLessonId { get; set; }

        public Guid? EducationDirectionId { get; set; }

        public Guid? DisciplineId { get; set; }

        public Guid? StudentGroupId { get; set; }

        public Guid? TimeNormId { get; set; }

        public string Semester { get; set; }
    }

    public class DisciplineLessonConductedSetBindingModel : PageSettingSetBinidingModel
    {
        [Required(ErrorMessage = "required")]
        public Guid DisciplineLessonId { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid StudentGroupId { get; set; }

        [Required(ErrorMessage = "required")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "required")]
        public string Subgroup { get; set; }
    }
}