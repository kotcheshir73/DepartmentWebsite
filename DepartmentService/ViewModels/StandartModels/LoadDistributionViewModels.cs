namespace DepartmentService.ViewModels
{
	public class LoadDistributionPageViewModel : PageViewModel<LoadDistributionViewModel> { }

	public class LoadDistributionViewModel
	{
		public long Id { get; set; }

		public long AcademicYearId { get; set; }

		public string AcademicYear { get; set; }
	}
}
