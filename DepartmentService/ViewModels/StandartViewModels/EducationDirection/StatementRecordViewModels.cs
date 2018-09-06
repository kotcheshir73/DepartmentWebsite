using System;

namespace DepartmentService.ViewModels
{
    public class StatementRecordPageViewModel : PageViewModel<StatementRecordViewModel> { }

    public class StatementRecordViewModel
    {
        public Guid Id { get; set; }

        public Guid StatementId { get; set; }

        public Guid StudentId { get; set; }

        public string Score { get; set; }
    }
}
