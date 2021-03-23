using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace BaseInterfaces.BindingModels
{
	public class LecturerDepartmentPostGetBindingModel : PageSettingGetBinidingModel { }

    public class LecturerDepartmentPostSetBindingModel : PageSettingSetBinidingModel
    {
        [Required(ErrorMessage = "required")]
        public string DepartmentPostTitle { get; set; }

        [Required(ErrorMessage = "required")]
        public int Order { get; set; }
    }
}