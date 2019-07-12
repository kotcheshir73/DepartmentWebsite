using System;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class StatementRecordGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? StatementId { get; set; }

        public Guid? StudentId { get; set; }
    }

    public class StatementRecordSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid StatementId { get; set; }

        public Guid StudentId { get; set; }

        public string Description { get; set; }

        public string Score { get; set; }
    }
}