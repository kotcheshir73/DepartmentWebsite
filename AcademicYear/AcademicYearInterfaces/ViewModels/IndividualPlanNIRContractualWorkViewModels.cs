using System;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class IndividualPlanNIRContractualWorkPageViewModel : PageSettingListViewModel<IndividualPlanNIRContractualWorkViewModel> { }

    public class IndividualPlanNIRContractualWorkViewModel : PageSettingElementViewModel
    {
        public Guid IndividualPlanId { get; set; }

        public string IndividualPlanName { get; set; }

        public string JobContent { get; set; }

        public string Post { get; set; }

        public string PlannedTerm { get; set; }

        public bool ReadyMark { get; set; }
    }
}