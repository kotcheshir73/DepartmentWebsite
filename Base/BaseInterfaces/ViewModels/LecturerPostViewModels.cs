using Tools.ViewModels;

namespace BaseInterfaces.ViewModels
{
    public class LecturerPostPageViewModel : PageSettingListViewModel<LecturerPostViewModel> { }
    
    public class LecturerPostViewModel : PageSettingElementViewModel
    {
        public string PostTitle { get; set; }

        public int Hours { get; set; }
    }
}