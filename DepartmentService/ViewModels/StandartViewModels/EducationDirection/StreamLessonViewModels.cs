using System;

namespace DepartmentService.ViewModels
{
    public class StreamLessonPageViewModel : PageViewModel<StreamLessonViewModel> { }

    public class StreamLessonViewModel
    {
        public Guid Id { get; set; }

        public Guid AcademicYearId { get; set; }

        public string AcademicYear { get; set; }

        public string StreamLessonName { get; set; }

        public decimal StreamLessonHours { get; set; }
    }
}
