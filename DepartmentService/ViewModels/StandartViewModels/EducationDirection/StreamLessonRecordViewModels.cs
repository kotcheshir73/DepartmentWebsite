using System;

namespace DepartmentService.ViewModels
{
    public class StreamLessonRecordPageViewModel : PageViewModel<StreamLessonRecordViewModel> { }

    public class StreamLessonRecordViewModel
    {
        public Guid Id { get; set; }

        public Guid StreamLessonId { get; set; }

        public Guid AcademicPlanRecordElementId { get; set; }

        public string StreamLessonName { get; set; }

        public string AcademicPlanRecordElementText { get; set; }

        public int Hours { get; set; }

        public bool IsMain { get; set; }
    }
}
