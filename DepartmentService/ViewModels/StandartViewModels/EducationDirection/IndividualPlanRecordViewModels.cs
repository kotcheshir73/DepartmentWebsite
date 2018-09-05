using System;

namespace DepartmentService.ViewModels
{
    public class IndividualPlanRecordPageViewModel : PageViewModel<IndividualPlanRecordViewModel> { }

    public class IndividualPlanRecordViewModel
    {
        public Guid Id { get; set; }

        public Guid IndividualPlanKindOfWorkId { get; set; }

        public Guid LecturerId { get; set; }

        public double PlanAutumn { get; set; }

        public double FactAutumn { get; set; }

        public double PlanSpring { get; set; }

        public double FactSpring { get; set; }
    }
}
