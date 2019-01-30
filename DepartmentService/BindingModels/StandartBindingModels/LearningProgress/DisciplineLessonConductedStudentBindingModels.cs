using DepartmentModel.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class DisciplineLessonConductedStudentGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? DisciplineLessonConductedId { get; set; }

        public Guid? StudentId { get; set; }

        public string Comment { get; set; }

        public DisciplineLessonStudentStatus? Status { get; set; }
    }

    public class DisciplineLessonConductedStudentSetBindingModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid DisciplineLessonConductedId { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid StudentId { get; set; }

        [Required(ErrorMessage = "required")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "required")]
        public string Status { get; set; }

        public decimal? Ball { get; set; }
    }
}
