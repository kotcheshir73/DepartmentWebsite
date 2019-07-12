using Tools.ViewModels;

namespace LaboratoryHeadInterfaces.ViewModels
{
    public class MaterialTechnicalValueGroupPageViewModel : PageSettingListViewModel<MaterialTechnicalValueGroupViewModel> { }

    public class MaterialTechnicalValueGroupViewModel : PageSettingElementViewModel
    {
        public string GroupName { get; set; }
        
        public int Order { get; set; }
    }
}