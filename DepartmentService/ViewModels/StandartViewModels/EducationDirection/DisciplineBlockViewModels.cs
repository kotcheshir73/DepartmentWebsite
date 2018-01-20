using System;

namespace DepartmentService.ViewModels
{
	public class DisciplineBlockPageViewModel : PageViewModel<DisciplineBlockViewModel> { }

	public class DisciplineBlockViewModel
	{
		public Guid Id { get; set; }

		public string Title { get; set; }
	}
}
