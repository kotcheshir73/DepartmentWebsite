using System;

namespace DepartmentService.ViewModels
{
	public class LoadDistributionPageViewModel : PageViewModel<LoadDistributionViewModel> { }

	public class LoadDistributionViewModel
	{
		public Guid Id { get; set; }

		public Guid AcademicYearId { get; set; }

		public string AcademicYear { get; set; }
	}
}
