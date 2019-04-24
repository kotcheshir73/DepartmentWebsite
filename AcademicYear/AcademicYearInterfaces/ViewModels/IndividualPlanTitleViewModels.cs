using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class IndividualPlanTitlePageViewModel : PageSettingListViewModel<IndividualPlanTitleViewModel> { }

    public class IndividualPlanTitleViewModel : PageSettingElementViewModel
    {
        public string Title { get; set; }

        public int Order { get; set; }
    }
}