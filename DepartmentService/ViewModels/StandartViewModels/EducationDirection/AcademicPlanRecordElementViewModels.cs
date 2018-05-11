using System;

namespace DepartmentService.ViewModels
{
    public class AcademicPlanRecordElementPageViewModel : PageViewModel<AcademicPlanRecordElementViewModel> { }

    public class AcademicPlanRecordElementViewModel
    {
        public Guid Id { get; set; }

        public Guid AcademicPlanRecordId { get; set; }

        public Guid KindOfLoadId { get; set; }

        public decimal Hours { get; set; }
    }
}
