﻿using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace BaseInterfaces.BindingModels
{

	public class DisciplineGetBindingModel : PageSettingGetBinidingModel
	{
		public Guid? DisciplineBlockId { get; set; }

        public string DisciplineName { get; set; }

        public Guid? ContingentId { get; set; }
    }

	public class DisciplineSetBindingModel : PageSettingSetBinidingModel
	{
		public Guid DisciplineBlockId { get; set; }

        [Required(ErrorMessage = "required")]
		public string DisciplineName { get; set; }

        [Required(ErrorMessage = "required")]
        public string DisciplineShortName { get; set; }

        [Required(ErrorMessage = "required")]
        public string DisciplineBlueAsteriskName { get; set; }

        public string DisciplineBlueAsteriskCode { get; set; }

        public string DisciplineBlueAsteriskPracticCode { get; set; }
    }
}