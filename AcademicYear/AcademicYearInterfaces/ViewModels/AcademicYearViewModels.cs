using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class AcademicYearPageViewModel : PageSettingListViewModel<AcademicYearViewModel> { }

	public class AcademicYearViewModel : PageSettingElementViewModel
	{
		public string Title { get; set; }
	}
}