using System;
using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
    public class LecturerWorkloadGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? AcademicYearId { get; set; }

        public Guid? LecturerId { get; set; }
    }

    public class LecturerWorkloadSetBindingModel :  PageSettingSetBinidingModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid LecturerId { get; set; }

        public double Workload { get; set; }
    }
}