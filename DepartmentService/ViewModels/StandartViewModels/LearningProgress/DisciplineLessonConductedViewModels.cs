using System;

namespace DepartmentService.ViewModels
{
    public class DisciplineLessonConductedPageViewModel : PageViewModel<DisciplineLessonConductedViewModel> { }

    public class DisciplineLessonConductedViewModel
    {
        public Guid Id { get; set; }

        public Guid DisciplineLessonId { get; set; }

        public Guid StudentGroupId { get; set; }

        public string Semester { get; set; }

        public string DisciplineLesson { get; set; }

        public string StudentGroup { get; set; }

        public DateTime Date { get; set; }

        public string Subgroup { get; set; }
    }
}
