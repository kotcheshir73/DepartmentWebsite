using System;

namespace DepartmentService.ViewModels
{
	public class StudentHistoryPageViewModel : PageViewModel<StudentHistoryViewModel> { }

	public class StudentHistoryViewModel
	{
		public Guid Id { get; set; }

		public Guid StudentId { get; set; }

		public string DateCreate { get; set; }

		public string TextMessage { get; set; }
	}
}
