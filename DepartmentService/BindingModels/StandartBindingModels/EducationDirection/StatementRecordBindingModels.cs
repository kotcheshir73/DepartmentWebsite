using System;

namespace DepartmentService.BindingModels
{
    public class StatementRecordGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? StatementId { get; set; }

        public Guid? StudentId { get; set; }
    }

    public class StatementRecordSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid StatementId { get; set; }

        public Guid StudentId { get; set; }

        public string Score { get; set; }
    }
}
