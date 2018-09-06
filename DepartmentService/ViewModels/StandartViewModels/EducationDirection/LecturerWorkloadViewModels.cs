using System;

namespace DepartmentService.ViewModels
{
    public class LecturerWorkloadPageViewModel : PageViewModel<LecturerWorkloadViewModel> { }

    public class LecturerWorkloadViewModel
    {
        public Guid Id { get; set; }

        public Guid AcademicYearId { get; set; }

        public Guid LecturerId { get; set; }

        public double Workload { get; set; }
    }
}
