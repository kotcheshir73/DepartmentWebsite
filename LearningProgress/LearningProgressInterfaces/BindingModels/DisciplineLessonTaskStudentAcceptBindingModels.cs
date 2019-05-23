using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace LearningProgressInterfaces.BindingModels
{
    public class DisciplineLessonTaskStudentAcceptGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? DisciplineLessonId { get; set; }

        public Guid? DisciplineLessonTaskId { get; set; }

        public Guid? StudentGroupId { get; set; }

        public Guid? StudentId { get; set; }
    }

    public class DisciplineLessonTaskStudentAcceptSetBindingModel : PageSettingSetBinidingModel
    {
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

        public double Score { get; set; }

        public string Comment { get; set; }

        public string Log { get; set; }
    }
}