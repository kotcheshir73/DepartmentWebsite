using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class MaterialTechnicalValueGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? ClassroomId { get; set; }

        public string InventoryNumber { get; set; }
    }

    public class MaterialTechnicalValueRecordBindingModel
    {
        public Guid Id { get; set; }

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
