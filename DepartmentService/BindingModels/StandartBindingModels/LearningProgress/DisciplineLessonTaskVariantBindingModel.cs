using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class DisciplineLessonTaskVariantGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? DisciplineLessonTaskId { get; set; }
    }

    public class DisciplineLessonTaskVariantRecordBindingModel
    {
        public Guid Id { get; set; }

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
