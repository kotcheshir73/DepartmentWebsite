using System;

namespace DepartmentService.ViewModels
{
    public class StatementRecordExtendedPageViewModel : PageViewModel<StatementRecordExtendedViewModel> { }

    public class StatementRecordExtendedViewModel
    {
        public Guid Id { get; set; }

        public Guid StatementRecordId { get; set; }

        public string Name { get; set; }
    }
}
