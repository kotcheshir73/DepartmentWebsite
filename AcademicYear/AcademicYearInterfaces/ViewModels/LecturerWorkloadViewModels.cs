using System;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class LecturerWorkloadPageViewModel : PageSettingListViewModel<LecturerWorkloadViewModel> { }

    public class LecturerWorkloadViewModel : PageSettingElementViewModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid LecturerId { get; set; }

        public double Workload { get; set; }
    }
}