using Interfaces.ViewModels;

namespace AuthenticationInterfaces.ViewModels
{
    public class RolePageViewModel : PageSettingListViewModel<RoleViewModel> { }

	public class RoleViewModel : PageSettingElementViewModel
	{
		public string RoleName { get; set; }
	}
}