using Enums;
using System;
using Tools.ViewModels;

namespace LearningProgressInterfaces.ViewModels
{
    public class DisciplineLessonConductedStudentPageViewModel : PageSettingListViewModel<DisciplineLessonConductedStudentViewModel> { }

    public class DisciplineLessonConductedStudentViewModel : PageSettingElementViewModel
    {
        public Guid DisciplineLessonConductedId { get; set; }

        public Guid StudentId { get; set; }

        public string DisciplineLesson { get; set; }

        public string Student { get; set; }

        public DisciplineLessonStudentStatus Status { get; set; }

        public string Comment { get; set; }

        public double? Ball { get; set; }

        public override string ToString()
        {
            return Student;
        }
    }
}