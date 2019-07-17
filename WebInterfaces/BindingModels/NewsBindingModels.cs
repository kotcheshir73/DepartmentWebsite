using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace WebInterfaces.BindingModels
{
    public class NewsGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? DepartmentUserId { get; set; }

        public string Tag { get; set; }
    }

    public class NewsSetBindingModel : PageSettingSetBinidingModel
    {
        [Required(ErrorMessage = "required")]
        public Guid DepartmentUserId { get; set; }

        [Required(ErrorMessage = "required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "required")]
        public string Body { get; set; }

        public string Tag { get; set; }
    }
}