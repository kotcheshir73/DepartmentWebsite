using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class RoleGetBindingModel : PageSettingBinidingModel
	{
		public Guid? Id { get; set; }
	}

	public class RoleRecordBindingModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "required")]
		public string RoleName { get; set; }
	}
}
