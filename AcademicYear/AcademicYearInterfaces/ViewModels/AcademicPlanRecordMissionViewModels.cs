using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class AcademicPlanRecordMissionPageViewModel : PageSettingListViewModel<AcademicPlanRecordMissionViewModel> { }

    public class AcademicPlanRecordMissionViewModel : PageSettingElementViewModel
    {
        [Required]
        [Display(Name = "Запись нагрузки*")]
        public Guid AcademicPlanRecordElementId { get; set; }

        [Required]
        [Display(Name = "Преподаватель*")]
        public Guid LecturerId { get; set; }

        public Guid DisciplineId { get; set; }

        [DisplayName("Нагрузка")]
        public string AcademicPlanRecordElementTitle { get; set; }

        [DisplayName("Преподаватель")]
        public string LecturerName { get; set; }

        public string DisciplineTitle { get; set; }

        [Required]
        [DisplayName("Часы")]
        [Display(Name = "Часы*")]
        public decimal Hours { get; set; }

        public string TimeNormShortName { get; set; }

        public string Semester { get; set; }
    }
}