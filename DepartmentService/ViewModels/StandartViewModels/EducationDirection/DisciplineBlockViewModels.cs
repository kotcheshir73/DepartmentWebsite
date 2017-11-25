namespace DepartmentService.ViewModels
{
	public class DisciplineBlockPageViewModel : PageViewModel<DisciplineBlockViewModel> { }

	public class DisciplineBlockViewModel
	{
		public long Id { get; set; }

		public string Title { get; set; }
	}
}
