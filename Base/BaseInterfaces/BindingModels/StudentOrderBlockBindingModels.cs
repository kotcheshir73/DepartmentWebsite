using Enums;
using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace BaseInterfaces.BindingModels
{
    public class StudentOrderBlockGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? StudentOrderId { get; set; }

        public Guid? EducationDirectionId { get; set; }

        public StudentOrderType? StudentOrderType { get; set; }
    }

    public class StudentOrderBlockSetBindingModel : PageSettingSetBinidingModel
    {
        [Required(ErrorMessage = "required")]
        public Guid StudentOrderId { get; set; }

        public Guid? EducationDirectionId { get; set; }

        [Required(ErrorMessage = "required")]
        public string StudentOrderType { get; set; }
    }
}