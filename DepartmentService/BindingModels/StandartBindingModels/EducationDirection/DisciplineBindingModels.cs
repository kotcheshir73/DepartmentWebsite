﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{

	public class DisciplineGetBindingModel : PageSettingBinidingModel
	{
		public Guid? Id { get; set; }

		public Guid? DisciplineBlockId { get; set; }
	}

	public class DisciplineRecordBindingModel
	{
		public Guid Id { get; set; }

		public Guid DisciplineBlockId { get; set; }

		[Required(ErrorMessage = "required")]
		public string DisciplineName { get; set; }

        [Required(ErrorMessage = "required")]
        public string DisciplineShortName { get; set; }
    }
}
