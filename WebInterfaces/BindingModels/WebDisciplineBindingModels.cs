﻿using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace WebInterfaces.BindingModels
{
    public class WebDisciplineGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid ContingentId { get; set; }

        public string DisciplineName { get; set; }
    }

    public class WebDisciplineSetBindingModel : PageSettingSetBinidingModel
    {
        [Required(ErrorMessage = "required")]
        public string DisciplinDescription { get; set; }
    }
}