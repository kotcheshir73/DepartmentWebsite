namespace DepartmentService.ViewModels
{
	public class KindOfLoadPageViewModel : PageViewModel<KindOfLoadViewModel> { }

	public class KindOfLoadViewModel
	{
		public long Id { get; set; }

		public string KindOfLoadName { get; set; }
		
		public string KindOfLoadType { get; set; }
	}
}
