using System;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class IndividualPlanNIRScientificArticlePageViewModel : PageSettingListViewModel<IndividualPlanNIRScientificArticleViewModel> { }

    public class IndividualPlanNIRScientificArticleViewModel : PageSettingElementViewModel
    {
        public Guid IndividualPlanId { get; set; }

        public string IndividualPlanName { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public string TypeOfPublication { get; set; }

        public double Volume { get; set; }

        public string Publishing { get; set; }

        public int Year { get; set; }

        public string Status { get; set; }
    }
}