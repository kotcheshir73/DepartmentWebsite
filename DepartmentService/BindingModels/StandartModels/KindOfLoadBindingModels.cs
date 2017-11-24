using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class KindOfLoadGetBindingModel : PageSettingBinidingModel
	{
		public long? Id { get; set; }
	}

	public class KindOfLoadRecordBindingModel
	{
		public long Id { get; set; }

		[Required(ErrorMessage = "required")]
		public string KindOfLoadName { get; set; }

		[Required(ErrorMessage = "required")]
		public string KindOfLoadType { get; set; }
	}
}
