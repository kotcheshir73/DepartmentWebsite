using System;
using Tools.ViewModels;

namespace BaseInterfaces.ViewModels
{
	public class StudentHistoryPageViewModel : PageSettingListViewModel<StudentHistoryViewModel> { }

	public class StudentHistoryViewModel : PageSettingElementViewModel
	{
		public Guid StudentId { get; set; }

		public string DateCreate { get; set; }

		public string TextMessage { get; set; }
	}
}