using System;

namespace DepartmentService.ViewModels
{
    public class MaterialTechnicalValueRecordPageViewModel : PageViewModel<MaterialTechnicalValueRecordViewModel> { }

    public class MaterialTechnicalValueRecordViewModel
    {
        public Guid Id { get; set; }

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
