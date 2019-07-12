using System;
using Tools.ViewModels;

namespace LearningProgressInterfaces.ViewModels
{
    public class DisciplineLessonConductedPageViewModel : PageSettingListViewModel<DisciplineLessonConductedViewModel> { }

    public class DisciplineLessonConductedViewModel : PageSettingElementViewModel
    {
        public Guid DisciplineLessonId { get; set; }

        public Guid StudentGroupId { get; set; }

        public string Semester { get; set; }

        public string DisciplineLesson { get; set; }

        public string StudentGroup { get; set; }

        public DateTime Date { get; set; }

        public string Subgroup { get; set; }

        public override string ToString()
        {
            return $"{DisciplineLesson}\n{Subgroup}";
        }
    }
}