using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class AcademicYearGetBindingModel : PageSettingBinidingModel
	{
		public long? Id { get; set; }
	}

	public class AcademicYearRecordBindingModel
	{
		public long Id { get; set; }

		[Required(ErrorMessage = "required")]
		public string Title { get; set; }
	}
}
