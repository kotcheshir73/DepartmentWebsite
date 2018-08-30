using System;

namespace DepartmentService.BindingModels
{
    public class AcademicPlanRecordMissionGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? AcademicPlanRecordElementId { get; set; }

        public Guid? LecturerId { get; set; }
    }

    public class AcademicPlanRecordMissionSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid AcademicPlanRecordElementId { get; set; }

        public Guid LecturerId { get; set; }

        public decimal Hours { get; set; }
    }
}
