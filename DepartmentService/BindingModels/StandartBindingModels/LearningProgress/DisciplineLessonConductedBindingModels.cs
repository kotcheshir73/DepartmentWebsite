using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class DisciplineLessonConductedGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? DisciplineLessonId { get; set; }

        public Guid? EducationFirectionId { get; set; }

        public Guid? DisciplineId { get; set; }

        public Guid? StudentGroupId { get; set; }

        public string Semester { get; set; }
    }

    public class DisciplineLessonConductedSetBindingModel
    {
        public Guid Id { get; set; }

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