using System;

namespace DepartmentService.ViewModels
{
    public class IndividualPlanTitlePageViewModel : PageViewModel<IndividualPlanTitleViewModel> { }

    public class IndividualPlanTitleViewModel
    {

        public Guid Id { get; set; }

        public string Title { get; set; }
    }
}
