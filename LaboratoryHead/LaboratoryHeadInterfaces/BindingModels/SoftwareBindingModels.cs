using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace LaboratoryHeadInterfaces.BindingModels
{
    public class SoftwareGetBindingModel : PageSettingGetBinidingModel { }

    public class SoftwareSetBindingModel : PageSettingSetBinidingModel
    {
        [Required(ErrorMessage = "required")]
        public string SoftwareName { get; set; }

        public string SoftwareDescription { get; set; }

        public string SoftwareKey { get; set; }

        public string SoftwareK { get; set; }
    }
}