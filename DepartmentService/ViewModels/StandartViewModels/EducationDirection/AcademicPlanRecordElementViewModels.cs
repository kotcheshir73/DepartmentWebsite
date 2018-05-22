using System;

namespace DepartmentService.ViewModels
{
    public class AcademicPlanRecordElementPageViewModel : PageViewModel<AcademicPlanRecordElementViewModel> { }

    public class AcademicPlanRecordElementViewModel
    {
        public Guid Id { get; set; }

        public Guid AcademicPlanRecordId { get; set; }

        public Guid TimeNormId { get; set; }

        public string Disciplne { get; set; }

        public string KindOfLoadName { get; set; }

        public decimal PlanHours { get; set; }

        public decimal FactHours { get; set; }
    }
}
