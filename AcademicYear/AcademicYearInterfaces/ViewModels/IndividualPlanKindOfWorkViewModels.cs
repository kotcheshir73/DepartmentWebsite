using System;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class IndividualPlanKindOfWorkPageViewModel : PageSettingListViewModel<IndividualPlanKindOfWorkViewModel> { }

    public class IndividualPlanKindOfWorkViewModel : PageSettingElementViewModel
    {
        public Guid IndividualPlanTitleId { get; set; }

        public  string IndividualPlanTitle { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public string TimeNormDescription { get; set; }
    }
}