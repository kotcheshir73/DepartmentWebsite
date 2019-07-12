using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace LaboratoryHeadInterfaces.BindingModels
{
    public class MaterialTechnicalValueRecordGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? MaterialTechnicalValueId { get; set; }

        public Guid? MaterialTechnicalValueGroupId { get; set; }
    }

    public class MaterialTechnicalValueRecordSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid MaterialTechnicalValueId { get; set; }
        
        public Guid MaterialTechnicalValueGroupId { get; set; }

        [Required(ErrorMessage = "required")]
        public string FieldName { get; set; }
        
        public string FieldValue { get; set; }

        [Required(ErrorMessage = "required")]
        public int Order { get; set; }
    }
}