using Tools.ViewModels;

namespace WebInterfaces.ViewModels
{
    public class WebContingentPageViewModel : PageSettingListViewModel<WebContingentViewModel> { }

    public class WebContingentViewModel : PageSettingElementViewModel
    {
        public string ContingentName { get; set; }

        public int Course { get; set; }
    }
}