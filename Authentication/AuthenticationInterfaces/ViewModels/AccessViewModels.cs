using DepartmentService.ViewModels;
using System;

namespace AuthenticationServiceInterfaces.ViewModels
{
	public class AccessPageViewModel : PageViewModel<AccessViewModel> { }

	public class AccessViewModel
	{
		public Guid Id { get; set; }

		public string Operation { get; set; }

		public string RoleName { get; set; }

		public string AccessType { get; set; }
	}
}