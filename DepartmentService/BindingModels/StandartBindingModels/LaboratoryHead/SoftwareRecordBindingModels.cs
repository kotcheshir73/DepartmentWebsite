using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class SoftwareRecordGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? MaterialTechnicalValueId { get; set; }
    }

    public class SoftwareRecordSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid MaterialTechnicalValueId { get; set; }

        public DateTime DateSetup { get; set; }

        [Required(ErrorMessage = "required")]
        public string SoftwareName { get; set; }

        public string SoftwareDescription { get; set; }

        public string SoftwareKey { get; set; }

        public string SoftwareK { get; set; }

        public string ClaimNumber { get; set; }
    }
}
