using System;

namespace DepartmentService.ViewModels
{
    public class AcademicPlanRecordMissionPageViewModel : PageViewModel<AcademicPlanRecordMissionViewModel> { }

    public class AcademicPlanRecordMissionViewModel
    {
        public Guid Id { get; set; }

        public Guid AcademicPlanRecordElementId { get; set; }

        public Guid LecturerId { get; set; }

        public decimal Hours { get; set; }
    }
}
