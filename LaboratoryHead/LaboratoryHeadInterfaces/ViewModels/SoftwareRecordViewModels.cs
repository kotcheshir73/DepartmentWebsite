using System;
using Tools.ViewModels;

namespace LaboratoryHeadInterfaces.ViewModels
{
    public class SoftwareRecordPageViewModel : PageSettingListViewModel<SoftwareRecordViewModel> { }

    public class SoftwareRecordViewModel : PageSettingElementViewModel
    {
        public Guid MaterialTechnicalValueId { get; set; }

        public Guid SoftwareId { get; set; }

        public string InventoryNumber { get; set; }

        public DateTime DateSetup { get; set; }

        public string SoftwareName { get; set; }

        public string SoftwareKey { get; set; }

        public string SetupDescription { get; set; }

        public string ClaimNumber { get; set; }
    }
}