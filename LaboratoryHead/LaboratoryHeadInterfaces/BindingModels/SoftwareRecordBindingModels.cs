using System;
using Tools.BindingModels;

namespace LaboratoryHeadInterfaces.BindingModels
{
    public class SoftwareRecordGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? MaterialTechnicalValueId { get; set; }

        public Guid? SoftwareId { get; set; }
    }

    public class SoftwareRecordSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid MaterialTechnicalValueId { get; set; }

        public Guid SoftwareId { get; set; }

        public DateTime DateSetup { get; set; }

        public string SetupDescription { get; set; }

        public string ClaimNumber { get; set; }
    }
}