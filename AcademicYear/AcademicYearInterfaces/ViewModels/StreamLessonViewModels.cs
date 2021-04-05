using System;
using System.ComponentModel;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class StreamLessonPageViewModel : PageSettingListViewModel<StreamLessonViewModel> { }

    public class StreamLessonViewModel : PageSettingElementViewModel
    {
        public Guid AcademicYearId { get; set; }

        public string AcademicYear { get; set; }

        [DisplayName("Название потока")]
        public string StreamLessonName { get; set; }

        [DisplayName("Часы")]
        public decimal StreamLessonHours { get; set; }

        [DisplayName("Семестр")]
        public string Semester { get; set; }
    }
}