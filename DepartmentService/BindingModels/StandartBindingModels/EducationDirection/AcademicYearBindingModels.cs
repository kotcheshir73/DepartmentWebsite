using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class AcademicYearGetBindingModel : PageSettingBinidingModel
	{
		public Guid? Id { get; set; }
	}

	public class AcademicYearSetBindingModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "required")]
		public string Title { get; set; }
	}
}
