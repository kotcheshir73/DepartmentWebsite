using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
    public class StreamLessonRecordGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? SteamLessonId { get; set; }
    }

    public class StreamLessonRecordSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid StreamLessonId { get; set; }

        public Guid AcademicPlanRecordElementId { get; set; }

        [Required(ErrorMessage = "required")]
        public bool IsMain { get; set; }
    }
}