using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace LaboratoryHeadInterfaces.BindingModels
{
    public class MaterialTechnicalValueGroupGetBindingModel : PageSettingGetBinidingModel { }

    public class MaterialTechnicalValueGroupSetBindingModel : PageSettingSetBinidingModel
    {
        [Required(ErrorMessage = "required")]
        public string GroupName { get; set; }

        [Required(ErrorMessage = "required")]
        public int Order { get; set; }
    }
}