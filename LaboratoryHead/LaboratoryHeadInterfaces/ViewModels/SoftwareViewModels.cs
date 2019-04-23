using Tools.ViewModels;

namespace LaboratoryHeadInterfaces.ViewModels
{
    public class SoftwarePageViewModel : PageSettingListViewModel<SoftwareViewModel> { }

    public class SoftwareViewModel : PageSettingElementViewModel
    {
        public string SoftwareName { get; set; }

        public string SoftwareDescription { get; set; }

        public string SoftwareKey { get; set; }

        public string SoftwareK { get; set; }
    }
}