using System;
using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
    public class DisciplineTimeDistributionRecordGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? DisciplineTimeDistributionId { get; set; }

        public Guid? TimeNormId { get; set; }
    }

    public class DisciplineTimeDistributionRecordSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid DisciplineTimeDistributionId { get; set; }

        public Guid TimeNormId { get; set; }

        public int WeekNumber { get; set; }

        public double Hours { get; set; }
    }
}