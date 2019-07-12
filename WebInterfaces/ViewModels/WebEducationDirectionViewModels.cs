using Tools.ViewModels;

namespace WebInterfaces.ViewModels
{
    public class WebEducationDirectionPageViewModel : PageSettingListViewModel<WebEducationDirectionViewModel> { }

    public class WebEducationDirectionViewModel : PageSettingElementViewModel
    {
        public string Cipher { get; set; }

        public string ShortName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}