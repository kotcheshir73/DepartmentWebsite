using DepartmentModel.Enums;
using System;

namespace DepartmentService.ViewModels.StandartViewModels.EducationDirection
{
    public class DisciplineLessonPageViewModel : PageViewModel<DisciplineLessonViewModel> { }

    public class DisciplineLessonViewModel
    {
        public Guid Id { get; set; }

        public Guid DisciplineId { get; set; }

        public LessonTypes LessonType { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }

        public byte[] DisciplineLessonFile { get; set; }
    }
}
