using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Tools.BindingModels;

namespace WebInterfaces.BindingModels
{
    public class EventGetBindingModel : PageSettingGetBinidingModel
    {
        public string Tag { get; set; }

    }

    public class EventSetBindingModel : PageSettingSetBinidingModel
    {
        [Required(ErrorMessage = "required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "required")]
        public string Content { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid DepartmentUserId { get; set; }

        public string Tag { get; set; }
        
    }
}
