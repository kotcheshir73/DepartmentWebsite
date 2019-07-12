using System;
using Tools.ViewModels;

namespace LearningProgressInterfaces.ViewModels
{
    public class DisciplineLessonTaskPageViewModel : PageSettingListViewModel<DisciplineLessonTaskViewModel> { }

    public class DisciplineLessonTaskViewModel : PageSettingElementViewModel
    {
        public Guid DisciplineLessonId { get; set; }

        public string DisciplineLessonTitle { get; set; }

        public bool IsNecessarily { get; set; }

        public string Task { get; set; }

        public int Order { get; set; }

        public double? MaxBall { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public override string ToString()
        {
            return Task;
        }
    }
}