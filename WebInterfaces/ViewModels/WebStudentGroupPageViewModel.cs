using Enums;
using Tools.ViewModels;

namespace WebInterfaces.ViewModels
{
    public class WebStudentGroupPageViewModel : PageSettingListViewModel<WebStudentGroupViewModel> { }

    public class WebStudentGroupViewModel : PageSettingElementViewModel
    {
        public string GroupName { get; set; }

        public AcademicCourse Course { get; set; }
    }
}