namespace DepartmentService.ViewModels
{
	public class StudentHistoryPageViewModel : PageViewModel<StudentHistoryViewModel> { }

	public class StudentHistoryViewModel
	{
		public long Id { get; set; }

		public string NumberOfBook { get; set; }

		public string DateCreate { get; set; }

		public string TextMessage { get; set; }
	}
}
