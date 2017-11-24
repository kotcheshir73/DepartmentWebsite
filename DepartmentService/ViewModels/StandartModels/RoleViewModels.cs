namespace DepartmentService.ViewModels
{
	public class RolePageViewModel : PageViewModel<RoleViewModel> { }

	public class RoleViewModel
	{
		public long Id { get; set; }

		public string RoleName { get; set; }
	}
}
