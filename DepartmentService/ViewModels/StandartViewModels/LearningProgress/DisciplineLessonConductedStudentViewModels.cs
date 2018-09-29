using DepartmentModel.Enums;
using System;

namespace DepartmentService.ViewModels
{
    public class DisciplineLessonConductedStudentPageViewModel : PageViewModel<DisciplineLessonConductedStudentViewModel> { }

    public class DisciplineLessonConductedStudentViewModel
    {
        public Guid Id { get; set; }

        public Guid DisciplineLessonConductedId { get; set; }

        public Guid StudentId { get; set; }

        public string DisciplineLesson { get; set; }

        public string Student { get; set; }

        public DisciplineLessonStudentStatus Status { get; set; }

        public string Comment { get; set; }

        public decimal? Ball { get; set; }
    }
}
