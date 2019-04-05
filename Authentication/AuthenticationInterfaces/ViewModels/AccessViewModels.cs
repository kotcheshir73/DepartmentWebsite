using Interfaces.ViewModels;
using System;

namespace AuthenticationInterfaces.ViewModels
{
    public class AccessPageViewModel : PageSettingListViewModel<AccessViewModel> { }

	public class AccessViewModel : PageSettingElementViewModel
	{
		public string Operation { get; set; }

		public string RoleName { get; set; }

		public string AccessType { get; set; }
	}
}