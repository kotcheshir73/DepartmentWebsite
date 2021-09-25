using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class LecturerWorkloadPageViewModel : PageSettingListViewModel<LecturerWorkloadViewModel> { }

    public class LecturerWorkloadViewModel : PageSettingElementViewModel
    {
        [Required]
        [Display(Name = "Учебный год*")]
        public Guid AcademicYearId { get; set; }

        [Required]
        [Display(Name = "Преподаватель*")]
        public Guid LecturerId { get; set; }

        public string AcademicYear { get; set; }

        [DisplayName("Преподаватель")]
        public string Lecturer { get; set; }

        [Required]
        [DisplayName("Нагрузка")]
        [Display(Name = "Нагрузка*")]
        public double Workload { get; set; }
    }
}