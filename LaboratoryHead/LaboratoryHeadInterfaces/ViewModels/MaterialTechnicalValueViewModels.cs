using System;
using Tools.ViewModels;

namespace LaboratoryHeadInterfaces.ViewModels
{
    public class MaterialTechnicalValuePageViewModel : PageSettingListViewModel<MaterialTechnicalValueViewModel> { }

    public class MaterialTechnicalValueViewModel : PageSettingElementViewModel
    {
        public Guid ClassroomId { get; set; }

        public string Classroom { get; set; }

        public DateTime DateInclude { get; set; }
        
        public string InventoryNumber { get; set; }
        
        public string FullName { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public decimal Cost { get; set; }

        public DateTime? DateDelete { get; set; }

        public string DeleteReason { get; set; }
    }
}