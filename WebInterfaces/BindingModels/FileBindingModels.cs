using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Tools.BindingModels;

namespace WebInterfaces.BindingModels
{
    public class FileGetBindingModel : PageSettingGetBinidingModel
    {
        
    }

    public class FileSetBindingModel : PageSettingSetBinidingModel
    {
        [Required(ErrorMessage = "required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "required")]
        public string Path { get; set; }

        [Required(ErrorMessage = "required")]
        public string Format { get; set; }
                
    }
}
