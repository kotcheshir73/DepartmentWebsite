using System;

namespace DepartmentService.ViewModels
{
    public class GraficPageViewModel : PageViewModel<GraficViewModel> { }

    public class GraficViewModel
    {
        public Guid Id { get; set; }

        public Guid AcademicPlanRecordId { get; set; }

        public Guid StudentGroupId { get; set; }

        public string Comment { get; set; }

        public string CommentWishesOfTeacher { get; set; }
        
        public string StudentGroupName { get; set; }

        public string DisciplineName { get; set; }

        public string Semester { get; set; }
    }
}
