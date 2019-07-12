using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace LearningProgressInterfaces.BindingModels
{
    public class DisciplineLessonTaskGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? DisciplineLessonId { get; set; }
    }

    public class DisciplineLessonTaskRecordBindingModel : PageSettingSetBinidingModel
    {
        [Required(ErrorMessage = "required")]
        public Guid DisciplineLessonId { get; set; }

        [Required(ErrorMessage = "required")]
        public string Task { get; set; }

        [Required(ErrorMessage = "required")]
        public bool IsNecessarily { get; set; }

        [Required(ErrorMessage = "required")]
        public int Order { get; set; }

        public double? MaxBall { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }
    }
}