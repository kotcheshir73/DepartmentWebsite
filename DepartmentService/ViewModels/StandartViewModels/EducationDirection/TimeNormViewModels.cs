using System;

namespace DepartmentService.ViewModels
{
	public class TimeNormPageViewModel : PageViewModel<TimeNormViewModel> { }

	public class TimeNormViewModel
	{
		public Guid Id { get; set; }

		public Guid KindOfLoadId { get; set; }

		public string Title { get; set; }

		public string KindOfLoadName { get; set; }

		public string Formula { get; set; }

		public decimal Hours { get; set; }
	}
}
