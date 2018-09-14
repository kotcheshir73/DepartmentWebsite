using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentService.BindingModels.StandartBindingModels.LearningProgress
{
    public class DisciplineLessonRecordGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? DisciplineLessonId { get; set; }

        public DateTime Date { get; set; }

        public string Subgroup { get; set; }
    }

    public class DisciplineLessonRecordSetBindingModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid DisciplineLessonId { get; set; }

        [Required(ErrorMessage = "required")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "required")]
        public string Subgroup { get; set; }
    }
}