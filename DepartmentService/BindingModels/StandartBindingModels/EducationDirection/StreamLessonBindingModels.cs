using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class StreamLessonGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? AcademicYearId { get; set; }
    }

    public class StreamLessonRecordBindingModel
    {
        public Guid Id { get; set; }

        public Guid AcademicYearId { get; set; }

        [Required(ErrorMessage = "required")]
        public string StreamLessonName { get; set; }
    }
}
