using System;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class StreamLessonPageViewModel : PageSettingListViewModel<StreamLessonViewModel> { }

    public class StreamLessonViewModel : PageSettingElementViewModel
    {
        public Guid AcademicYearId { get; set; }

        public string AcademicYear { get; set; }

        public string Semester { get; set; }

        public string StreamLessonName { get; set; }

        public decimal StreamLessonHours { get; set; }
    }
}