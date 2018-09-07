using System;

namespace DepartmentService.ViewModels
{
    public class StatementPageViewModel : PageViewModel<StatementViewModel> { }

    public class StatementViewModel
    {
        public Guid Id { get; set; }

        public Guid LecturerId { get; set; }

        public Guid AcademicPlanRecordId { get; set; }

        public Guid StudentGroupId { get; set; }

        public string Course { get; set; }

        public string TypeOfTest { get; set; }

        public string Semester { get; set; }

        public DateTime? Date { get; set; }
    }
}
