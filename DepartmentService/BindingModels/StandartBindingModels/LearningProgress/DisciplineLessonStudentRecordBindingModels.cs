using DepartmentModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentService.BindingModels.StandartBindingModels.LearningProgress
{
    public class DisciplineLessonStudentRecordGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? DisciplineLessonRecordId { get; set; }

        public Guid? StudentId { get; set; }

        public string Comment { get; set; }

        public int? Ball { get; set; }

        public DisciplineLessonStudentStatus Status { get; set; }
    }

    public class DisciplineLessonStudentRecordSetBindingModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid DisciplineLessonRecordId { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid StudentId { get; set; }

        [Required(ErrorMessage = "required")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "required")]
        public DisciplineLessonStudentStatus Status { get; set; }

        public int Ball { get; set; }
    }
}
