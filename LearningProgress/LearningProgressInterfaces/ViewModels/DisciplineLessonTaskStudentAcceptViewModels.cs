using Enums;
using System;
using Tools.ViewModels;

namespace LearningProgressInterfaces.ViewModels
{
    public class DisciplineLessonTaskStudentAcceptPageViewModel : PageSettingListViewModel<DisciplineLessonTaskStudentAcceptViewModel> { }

    public class DisciplineLessonTaskStudentAcceptViewModel : PageSettingElementViewModel
    {
        public Guid DisciplineLessonTaskId { get; set; }

        public Guid StudentId { get; set; }

        public string DisciplineLessonTask { get; set; }

        public string Student { get; set; }

        public DisciplineLessonTaskStudentResult Result { get; set; }

        public string Task { get; set; }

        public DateTime DateAccept { get; set; }
        
        public decimal Score { get; set; }
        
        public string Comment { get; set; }
        
        public string Log { get; set; }
    }
}