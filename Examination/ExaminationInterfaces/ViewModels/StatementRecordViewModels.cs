using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class StatementRecordPageViewModel : PageSettingListViewModel<StatementRecordViewModel> { }

    public class StatementRecordViewModel : PageSettingElementViewModel
    {
        public Guid StatementId { get; set; }

        public Guid StudentId { get; set; }

        public string StatementName { get; set; }

        public string StudentName { get; set; }

        public string Description { get; set; }

        public string Score { get; set; }
    }
}