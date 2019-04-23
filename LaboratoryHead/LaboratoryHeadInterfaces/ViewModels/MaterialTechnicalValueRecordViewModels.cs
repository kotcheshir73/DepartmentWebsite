using System;
using Tools.ViewModels;

namespace LaboratoryHeadInterfaces.ViewModels
{
    public class MaterialTechnicalValueRecordPageViewModel : PageSettingListViewModel<MaterialTechnicalValueRecordViewModel> { }

    public class MaterialTechnicalValueRecordViewModel : PageSettingElementViewModel
    {
        public Guid MaterialTechnicalValueId { get; set; }

        public string InventoryNumber { get; set; }

        public Guid MaterialTechnicalValueGroupId { get; set; }

        public string GroupName { get; set; }

        public int GroupOrder { get; set; }

        public string FieldName { get; set; }

        public string FieldValue { get; set; }

        public int Order { get; set; }
    }
}