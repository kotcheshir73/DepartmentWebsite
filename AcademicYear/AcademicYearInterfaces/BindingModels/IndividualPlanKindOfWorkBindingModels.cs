using System;
using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
    public class IndividualPlanKindOfWorkGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? IndividualPlanTitleId { get; set; }

        public string Name { get; set; }
    }

    public class IndividualPlanKindOfWorkSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid IndividualPlanTitleId { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public string TimeNormDescription { get; set; }
    }
}