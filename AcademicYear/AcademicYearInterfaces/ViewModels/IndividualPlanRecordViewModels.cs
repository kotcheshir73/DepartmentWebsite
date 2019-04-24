using System;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class IndividualPlanRecordPageViewModel : PageSettingListViewModel<IndividualPlanRecordViewModel> { }

    public class IndividualPlanRecordViewModel : PageSettingElementViewModel
    {
        public Guid IndividualPlanKindOfWorkId { get; set; }

        public Guid IndividualPlanId { get; set; }

        public string IndividualPlanKindOfWorkName { get; set; }

        public string IndividualPlanName { get; set; }

        public string Name { get; set; }

        public string TimeNormDescription { get; set; }

        public double PlanAutumn { get; set; }

        public double FactAutumn { get; set; }

        public double PlanSpring { get; set; }

        public double FactSpring { get; set; }
    }
}