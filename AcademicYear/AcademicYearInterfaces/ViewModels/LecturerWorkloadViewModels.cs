using System;
using System.ComponentModel;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class LecturerWorkloadPageViewModel : PageSettingListViewModel<LecturerWorkloadViewModel> { }

    public class LecturerWorkloadViewModel : PageSettingElementViewModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid LecturerId { get; set; }

        public string AcademicYear { get; set; }

        [DisplayName("Преподаватель")]
        public string Lecturer { get; set; }

        [DisplayName("Нагрузка")]
        public double Workload { get; set; }
    }
}