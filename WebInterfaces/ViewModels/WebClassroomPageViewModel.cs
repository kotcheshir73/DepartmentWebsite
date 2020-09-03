using Tools.ViewModels;

namespace WebInterfaces.ViewModels
{
    public class WebClassroomPageViewModel : PageSettingListViewModel<WebClassroomViewModel> { }

    public class WebClassroomViewModel : PageSettingElementViewModel
    {
        public string Number { get; set; }
    }
}