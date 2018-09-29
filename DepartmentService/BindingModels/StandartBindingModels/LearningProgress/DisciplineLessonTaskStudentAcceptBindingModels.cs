using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class DisciplineLessonTaskStudentAcceptGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? DisciplineLessonId { get; set; }

        public Guid? DisciplineLessonTaskId { get; set; }

        public Guid? StudentGroupId { get; set; }

        public Guid? StudentId { get; set; }
    }

    public class DisciplineLessonTaskStudentAcceptSetBindingModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid DisciplineLessonTaskId { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid StudentId { get; set; }

        [Required(ErrorMessage = "required")]
        public string Result { get; set; }

        [Required(ErrorMessage = "required")]
        public string Task { get; set; }

        [Required(ErrorMessage = "required")]
        public DateTime DateAccept { get; set; }

        public decimal Score { get; set; }

        public string Comment { get; set; }

        public string Log { get; set; }
    }
}
