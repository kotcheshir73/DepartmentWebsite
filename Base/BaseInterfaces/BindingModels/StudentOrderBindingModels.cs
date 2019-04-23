using Enums;
using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace BaseInterfaces.BindingModels
{
    public class StudentOrderGetBindingModel : PageSettingGetBinidingModel
    {
        public StudentOrderType? StudentOrderType { get; set; }
    }

    public class StudentOrderSetBindingModel : PageSettingSetBinidingModel
    {
        [Required(ErrorMessage = "required")]
        public string OrderNumber { get; set; }

        [Required(ErrorMessage = "required")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "required")]
        public string StudentOrderType { get; set; }
    }
}