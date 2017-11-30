﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class ExaminationRecordRecordBindingModel : ScheduleRecordBindingModel
    {
		[Required(ErrorMessage = "required")]
		public DateTime DateConsultation { get; set; }

		[Required(ErrorMessage = "required")]
		public DateTime DateExamination { get; set; }
	}
}