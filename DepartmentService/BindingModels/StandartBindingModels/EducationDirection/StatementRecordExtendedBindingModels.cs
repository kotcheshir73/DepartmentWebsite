using System;

namespace DepartmentService.BindingModels
{
    public class StatementRecordExtendedGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? StatementRecordId { get; set; }
    }

    public class StatementRecordExtendedSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid StatementRecordId { get; set; }

        public string Name { get; set; }
    }
}
