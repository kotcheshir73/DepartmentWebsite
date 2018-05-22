using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class StreamLessonGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? AcademicYearId { get; set; }
    }

    public class StreamLessonSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid AcademicYearId { get; set; }

        [Required(ErrorMessage = "required")]
        public decimal StreamLessonHours { get; set; }

        [Required(ErrorMessage = "required")]
        public string StreamLessonName { get; set; }
    }
}
