using System;

namespace DepartmentService.ViewModels
{
    public class DisciplineLessonTaskVariantPageViewModel : PageViewModel<DisciplineLessonTaskVariantViewModel> { }

    public class DisciplineLessonTaskVariantViewModel
    {
        public Guid Id { get; set; }

        public Guid DisciplineLessonTaskId { get; set; }

        public string VariantNumber { get; set; }

        public string VariantTask { get; set; }

        public int Order { get; set; }
    }
}
