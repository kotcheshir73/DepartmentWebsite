using DepartmentService.ViewModels;
using System;

namespace AuthenticationServiceInterfaces.ViewModels
{
	public class RolePageViewModel : PageViewModel<RoleViewModel> { }

	public class RoleViewModel
	{
		public Guid Id { get; set; }

		public string RoleName { get; set; }
	}
}