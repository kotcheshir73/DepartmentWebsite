﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class MaterialTechnicalValueRecordGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? MaterialTechnicalValueId { get; set; }

        public Guid? MaterialTechnicalValueGroupId { get; set; }
    }

    public class MaterialTechnicalValueRecordSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid MaterialTechnicalValueId { get; set; }
        
        public Guid MaterialTechnicalValueGroupId { get; set; }

        [Required(ErrorMessage = "required")]
        public string FieldName { get; set; }
        
        public string FieldValue { get; set; }

        [Required(ErrorMessage = "required")]
        public int Order { get; set; }
    }
}
