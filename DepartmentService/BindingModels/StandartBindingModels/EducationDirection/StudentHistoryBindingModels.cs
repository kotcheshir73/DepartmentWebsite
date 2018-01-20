using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class StudentHistoryGetBindingModel : PageSettingBinidingModel
	{
		public Guid? StudetnId { get; set; }

		public Guid? Id { get; set; }
	}

	public class StudentHistoryRecordBindingModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "required")]
		public Guid StudetnId { get; set; }

		public DateTime DateCreate { get; set; }

		[Required(ErrorMessage = "required")]
		public string TextMessage { get; set; }
	}
}
