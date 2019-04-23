using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace LaboratoryHeadInterfaces.BindingModels
{
    public class MaterialTechnicalValueGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? ClassroomId { get; set; }

        public string InventoryNumber { get; set; }
    }

    public class MaterialTechnicalValueSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid ClassroomId { get; set; }

        public DateTime DateInclude { get; set; }

        [Required(ErrorMessage = "required")]
        public string InventoryNumber { get; set; }

        [Required(ErrorMessage = "required")]
        public string FullName { get; set; }
        
        public string Description { get; set; }
        
        public string Location { get; set; }

        public decimal Cost { get; set; }

        public DateTime? DateDelete { get; set; }

        public string DeleteReason { get; set; }
    }
}