using System;

namespace DepartmentService.ViewModels
{
	public class RolePageViewModel : PageViewModel<RoleViewModel> { }

	public class RoleViewModel
	{
		public Guid Id { get; set; }

		public string RoleName { get; set; }
	}
}
