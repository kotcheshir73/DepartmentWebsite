using System;

namespace DepartmentService.BindingModels
{
    public class LecturerWorkloadGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? AcademicYearId { get; set; }

        public Guid? LecturerId { get; set; }
    }

    public class LecturerWorkloadSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid AcademicYearId { get; set; }

        public Guid LecturerId { get; set; }

        public double Workload { get; set; }
    }
}
