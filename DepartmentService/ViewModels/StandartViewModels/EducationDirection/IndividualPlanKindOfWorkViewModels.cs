using System;

namespace DepartmentService.ViewModels
{
    public class IndividualPlanKindOfWorkPageViewModel : PageViewModel<IndividualPlanKindOfWorkViewModel> { }

    public class IndividualPlanKindOfWorkViewModel
    {
        public Guid Id { get; set; }

        public Guid IndividualPlanTitleId { get; set; }

        public string Name { get; set; }

        public string TimeNormDescription { get; set; }
    }
}
