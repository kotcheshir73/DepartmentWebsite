using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
    public class StreamLessonGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? AcademicYearId { get; set; }
    }

    public class StreamLessonSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid AcademicYearId { get; set; }

        [Required(ErrorMessage = "required")]
        public decimal StreamLessonHours { get; set; }

        [Required(ErrorMessage = "required")]
        public string StreamLessonName { get; set; }
    }
}