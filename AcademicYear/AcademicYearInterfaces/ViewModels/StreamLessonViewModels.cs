using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class StreamLessonPageViewModel : PageSettingListViewModel<StreamLessonViewModel> { }

    public class StreamLessonViewModel : PageSettingElementViewModel
    {
        [Required]
        [Display(Name = "Учебный год*")]
        public Guid AcademicYearId { get; set; }

        public string AcademicYear { get; set; }

        [Required]
        [DisplayName("Название потока")]
        [Display(Name = "Название*")]
        public string StreamLessonName { get; set; }

        [Required]
        [DisplayName("Часы")]
        [Display(Name = "Часы*")]
        public decimal StreamLessonHours { get; set; }

        [DisplayName("Семестр")]
        [Display(Name = "Семестр")]
        public string Semester { get; set; }
    }
}