using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace LearningProgressInterfaces.BindingModels
{
    public class DisciplineLessonTaskVariantGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? DisciplineLessonTaskId { get; set; }
    }

    public class DisciplineLessonTaskVariantRecordBindingModel : PageSettingSetBinidingModel
    {
        [Required(ErrorMessage = "required")]
        public Guid DisciplineLessonTaskId { get; set; }

        [Required(ErrorMessage = "required")]
        public string VariantNumber { get; set; }

        [Required(ErrorMessage = "required")]
        public string VariantTask { get; set; }

        [Required(ErrorMessage = "required")]
        public int Order { get; set; }
    }
}