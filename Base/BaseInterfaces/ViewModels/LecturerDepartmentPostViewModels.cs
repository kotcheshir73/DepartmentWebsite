using Tools.ViewModels;

namespace BaseInterfaces.ViewModels
{
	public class LecturerDepartmentPostPageViewModel : PageSettingListViewModel<LecturerDepartmentPostViewModel> { }

    public class LecturerDepartmentPostViewModel : PageSettingElementViewModel
    {
        public string DepartmentPostTitle { get; set; }

        public int Order { get; set; }
    }
}