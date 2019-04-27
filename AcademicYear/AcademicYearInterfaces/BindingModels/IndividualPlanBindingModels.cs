using System;
using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
    public class IndividualPlanGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? AcademicYearId { get; set; }

        public Guid? LecturerId { get; set; }
    }

    public class IndividualPlanSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid LecturerId { get; set; }
    }
}