using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class MaterialTechnicalValueGroupGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }
    }

    public class MaterialTechnicalValueGroupSetBindingModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "required")]
        public string GroupName { get; set; }

        [Required(ErrorMessage = "required")]
        public int Order { get; set; }
    }
}
