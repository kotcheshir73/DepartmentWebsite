using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class DisciplineBlockGetBindingModel : PageSettingBinidingModel
	{
		public long? Id { get; set; }
	}

	public class DisciplineBlockRecordBindingModel
	{
		public long Id { get; set; }

		[Required(ErrorMessage = "required")]
		public string Title { get; set; }
	}
}
