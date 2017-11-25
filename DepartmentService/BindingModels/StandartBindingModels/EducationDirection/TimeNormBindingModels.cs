using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class TimeNormGetBindingModel : PageSettingBinidingModel
	{
		public long? Id { get; set; }
	}

	public class TimeNormRecordBindingModel
	{
		public long Id { get; set; }

		[Required(ErrorMessage = "required")]
		public string Title { get; set; }

		public long KindOfLoadId { get; set; }

		public string Formula { get; set; }

		public decimal Hours { get; set; }
	}
}
