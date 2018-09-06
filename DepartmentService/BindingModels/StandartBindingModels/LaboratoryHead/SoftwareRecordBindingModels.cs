using System;

namespace DepartmentService.BindingModels
{
    public class SoftwareRecordGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? MaterialTechnicalValueId { get; set; }

        public Guid? SoftwareId { get; set; }
    }

    public class SoftwareRecordSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid MaterialTechnicalValueId { get; set; }

        public Guid SoftwareId { get; set; }

        public DateTime DateSetup { get; set; }

        public string SetupDescription { get; set; }

        public string ClaimNumber { get; set; }
    }
}
