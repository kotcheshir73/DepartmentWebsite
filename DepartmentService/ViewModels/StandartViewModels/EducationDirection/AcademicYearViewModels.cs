﻿namespace DepartmentService.ViewModels
{
	public class AcademicYearPageViewModel : PageViewModel<AcademicYearViewModel> { }

	public class AcademicYearViewModel
	{
		public long Id { get; set; }

		public string Title { get; set; }
	}
}