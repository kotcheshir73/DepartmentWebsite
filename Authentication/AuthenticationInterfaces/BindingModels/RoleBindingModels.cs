using Interfaces.BindingModels;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationInterfaces.BindingModels
{
    public class RoleGetBindingModel : PageSettingGetBinidingModel { }

    public class RoleSetBindingModel : PageSettingSetBinidingModel
    {
        [Required(ErrorMessage = "required")]
        public string RoleName { get; set; }
    }
}