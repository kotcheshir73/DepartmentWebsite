using Enums;
using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace LearningProgressInterfaces.BindingModels
{
    public class DisciplineLessonConductedStudentGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? DisciplineLessonConductedId { get; set; }

        public Guid? StudentId { get; set; }

        public string Comment { get; set; }

        public DisciplineLessonStudentStatus? Status { get; set; }
    }

    public class DisciplineLessonConductedStudentSetBindingModel : PageSettingSetBinidingModel
    {
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