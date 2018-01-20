using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class DisciplineBlockGetBindingModel : PageSettingBinidingModel
	{
		public Guid? Id { get; set; }
	}

	public class DisciplineBlockRecordBindingModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "required")]
		public string Title { get; set; }
	}
}
