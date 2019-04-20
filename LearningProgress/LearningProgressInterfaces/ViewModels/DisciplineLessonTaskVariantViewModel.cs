using System;
using Tools.ViewModels;

namespace LearningProgressInterfaces.ViewModels
{
    public class DisciplineLessonTaskVariantPageViewModel : PageSettingListViewModel<DisciplineLessonTaskVariantViewModel> { }

    public class DisciplineLessonTaskVariantViewModel : PageSettingElementViewModel
    {
        public Guid DisciplineLessonTaskId { get; set; }

        public string DisciplineLessonTaskTask { get; set; }

        public string VariantNumber { get; set; }

        public string VariantTask { get; set; }

        public int Order { get; set; }
    }
}