using System;

namespace DepartmentService.ViewModels
{
    public class IndividualPlanNIRContractualWorkPageViewModel : PageViewModel<IndividualPlanNIRContractualWorkViewModel> { }

    public class IndividualPlanNIRContractualWorkViewModel
    {
        public Guid Id { get; set; }

        public Guid LecturerId { get; set; }

        public string LecturerName { get; set; }

        public string JobContent { get; set; }

        public string Post { get; set; }

        public string PlannedTerm { get; set; }

        public string ReadyMark { get; set; }
    }
}
