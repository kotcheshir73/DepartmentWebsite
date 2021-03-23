using Tools.ViewModels;

namespace BaseInterfaces.ViewModels
{
    public class LecturerStudyPostPageViewModel : PageSettingListViewModel<LecturerStudyPostViewModel> { }
    
    public class LecturerStudyPostViewModel : PageSettingElementViewModel
    {
        public string StudyPostTitle { get; set; }

        public int Hours { get; set; }
    }
}