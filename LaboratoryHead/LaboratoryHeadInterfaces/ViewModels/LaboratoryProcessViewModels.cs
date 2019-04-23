using System;
using Tools.ViewModels;

namespace LaboratoryHeadInterfaces.ViewModels
{
    public class LaboratoryProcessSoftwareRecordPageViewModel : PageSettingListViewModel<LaboratoryProcessSoftwareRecordsViewModels, LaboratoryProcessMaterialTechincalValuesViewModels> { }

    public class LaboratoryProcessSoftwareRecordsViewModels
    {
        public DateTime DateSetup { get; set; }

        public string SoftwareName { get; set; }

        public string SoftwareDescription { get; set; }

        public string SoftwareKey { get; set; }

        public string ClaimNumber { get; set; }
    }

    public class LaboratoryProcessMaterialTechincalValuesViewModels
    {
        public string InventoryNumber { get; set; }
    }
}
