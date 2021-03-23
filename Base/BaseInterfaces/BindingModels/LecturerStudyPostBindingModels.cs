using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace BaseInterfaces.BindingModels
{
    public class LecturerStudyPostGetBindingModel : PageSettingGetBinidingModel { }

    public class LecturerStudyPostSetBindingModel : PageSettingSetBinidingModel
    {
        [Required(ErrorMessage = "required")]
        public string StudyPostTitle { get; set; }

        [Required(ErrorMessage = "required")]
        public int Hours { get; set; }
    }
}