namespace DepartmentService.ViewModels
{
	public class AccessPageViewModel : PageViewModel<AccessViewModel> { }

	public class AccessViewModel
	{
		public long Id { get; set; }

		public string Operation { get; set; }

		public string RoleName { get; set; }

		public string AccessType { get; set; }
	}
}
