using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentService.BindingModels.StandartBindingModels.EducationDirection
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
    }
}
