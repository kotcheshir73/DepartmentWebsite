using System;

namespace DepartmentService.ViewModels
{
    public class SoftwareRecordPageViewModel : PageViewModel<SoftwareRecordViewModel> { }

    public class SoftwareRecordViewModel
    {
        public Guid Id { get; set; }

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
