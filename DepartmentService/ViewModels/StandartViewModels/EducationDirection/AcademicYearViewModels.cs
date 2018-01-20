using System;

namespace DepartmentService.ViewModels
{
	public class AcademicYearPageViewModel : PageViewModel<AcademicYearViewModel> { }

	public class AcademicYearViewModel
	{
		public Guid Id { get; set; }

		public string Title { get; set; }
	}
}
