using DepartmentModel.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels.StandartBindingModels.EducationDirection
{
    public class DisciplineLessonGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? DisciplineId { get; set; }
    }

    public class DisciplineLessonRecordBindingModel : PageSettingBinidingModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid DisciplineId { get; set; }

        [Required(ErrorMessage = "required")]
        public LessonTypes LessonType { get; set; }

        [Required(ErrorMessage = "required")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "required")]
        public int Order { get; set; }

        public byte[] DisciplineLessonFile { get; set; }

    }
}
