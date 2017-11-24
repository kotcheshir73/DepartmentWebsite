using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class StudentHistoryGetBindingModel : PageSettingBinidingModel
	{
		public string NumberOfBook { get; set; }

		public long? Id { get; set; }
	}

	public class StudentHistoryRecordBindingModel
	{
		public long Id { get; set; }

		[Required(ErrorMessage = "required")]
		public string NumberOfBook { get; set; }

		public DateTime DateCreate { get; set; }

		[Required(ErrorMessage = "required")]
		public string TextMessage { get; set; }
	}
}
