using DepartmentModel.Enums;
using System;

namespace DepartmentService.ViewModels
{
    public class DisciplineLessonPageViewModel : PageViewModel<DisciplineLessonViewModel> { }

    public class DisciplineLessonViewModel
    {
        public Guid Id { get; set; }

        public Guid AcademicYearId { get; set; }

        public Guid DisciplineId { get; set; }

        public string AcademicYear { get; set; }

        public Semesters Semester { get; set; }

        public DisciplineLessonTypes LessonType { get; set; }

        public string Discipline { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }

        public int CountOfPairs { get; set; }

        public int CountTasks { get; set; }

        public DateTime? Date { get; set; }

        public byte[] DisciplineLessonFile { get; set; }
    }
}
