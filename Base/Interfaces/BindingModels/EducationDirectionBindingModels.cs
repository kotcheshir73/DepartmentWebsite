using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace Interfaces.BindingModels
{
    public class EducationDirectionGetBindingModel : PageSettingGetBinidingModel { }

    public class EducationDirectionSetBindingModel : PageSettingSetBinidingModel
    {
        [Required(ErrorMessage = "required")]
        public string Cipher { get; set; }

        [Required(ErrorMessage = "required")]
        public string ShortName { get; set; }

        [Required(ErrorMessage = "required")]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}