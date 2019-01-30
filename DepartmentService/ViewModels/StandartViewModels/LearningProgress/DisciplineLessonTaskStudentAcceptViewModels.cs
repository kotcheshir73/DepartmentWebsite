using DepartmentModel.Enums;
using System;

namespace DepartmentService.ViewModels
{
    public class DisciplineLessonTaskStudentAcceptPageViewModel : PageViewModel<DisciplineLessonTaskStudentAcceptViewModel> { }

    public class DisciplineLessonTaskStudentAcceptViewModel
    {
        public Guid Id { get; set; }

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
