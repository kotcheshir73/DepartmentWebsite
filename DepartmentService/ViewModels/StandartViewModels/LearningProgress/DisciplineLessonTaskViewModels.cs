using System;

namespace DepartmentService.ViewModels
{
    public class DisciplineLessonTaskPageViewModel : PageViewModel<DisciplineLessonTaskViewModel> { }

    public class DisciplineLessonTaskViewModel
    {
        public Guid Id { get; set; }

        public Guid DisciplineLessonId { get; set; }

        public string DisciplineLessonTitle { get; set; }

        public bool IsNecessarily { get; set; }

        public string Task { get; set; }

        public int Order { get; set; }

        public decimal? MaxBall { get; set; }

        public string Description { get; set; }

        public byte?[] Image { get; set; }
    }
}
