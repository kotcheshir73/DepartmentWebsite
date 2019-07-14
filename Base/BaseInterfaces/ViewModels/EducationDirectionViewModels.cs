using Tools.ViewModels;

namespace BaseInterfaces.ViewModels
{
    public class EducationDirectionPageViewModel : PageSettingListViewModel<EducationDirectionViewModel> { }

	public class EducationDirectionViewModel : PageSettingElementViewModel
    {
        public string Cipher { get; set; }

        public string ShortName { get; set; }

        public string Title { get; set; }

        public string Qualification { get; set; }
        
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Cipher} - {ShortName}";
        }
    }
}