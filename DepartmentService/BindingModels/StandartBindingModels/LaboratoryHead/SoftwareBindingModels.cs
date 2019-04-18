using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class SoftwareGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }
    }

    public class SoftwareSetBindingModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "required")]
        public string SoftwareName { get; set; }

        public string SoftwareDescription { get; set; }

        public string SoftwareKey { get; set; }

        public string SoftwareK { get; set; }
    }
}
