using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class DisciplineLessonTaskGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? DisciplineLessonId { get; set; }
    }

    public class DisciplineLessonTaskRecordBindingModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid DisciplineLessonId { get; set; }

        [Required(ErrorMessage = "required")]
        public string Task { get; set; }

        [Required(ErrorMessage = "required")]
        public bool IsNecessarily { get; set; }

        [Required(ErrorMessage = "required")]
        public int Order { get; set; }

        public decimal? MaxBall { get; set; }

        public string Description { get; set; }

        public byte?[] Image { get; set; }
    }
}
