using DepartmentService.BindingModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationServiceInterfaces.BindingModels
{
    public class RoleGetBindingModel : PageSettingBinidingModel
	{
		public Guid? Id { get; set; }
	}

	public class RoleSetBindingModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "required")]
		public string RoleName { get; set; }
	}
}