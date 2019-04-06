﻿using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace Interfaces.BindingModels
{
    public class LecturerPostGetBindingModel : PageSettingGetBinidingModel { }

    public class LecturerPostSetBindingModel : PageSettingSetBinidingModel
    {
        [Required(ErrorMessage = "required")]
        public string PostTitle { get; set; }

        [Required(ErrorMessage = "required")]
        public int Hours { get; set; }
    }
}