using System;
using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
    public class DisciplineTimeDistributionClassroomGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? DisciplineTimeDistributionId { get; set; }

        public Guid? TimeNormId { get; set; }
    }

    public class DisciplineTimeDistributionClassroomSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid DisciplineTimeDistributionId { get; set; }

        public Guid TimeNormId { get; set; }

        public string ClassroomDescription { get; set; }
    }
}