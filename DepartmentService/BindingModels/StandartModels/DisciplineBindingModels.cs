using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{

	public class DisciplineGetBindingModel : PageSettingBinidingModel
	{
		public long? Id { get; set; }

		public long? DisciplineBlockId { get; set; }
	}

	public class DisciplineRecordBindingModel
	{
		public long Id { get; set; }

		public long DisciplineBlockId { get; set; }

		[Required(ErrorMessage = "required")]
		public string DisciplineName { get; set; }
	}
}
