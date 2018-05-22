using System;

namespace DepartmentService.BindingModels
{
    public class AcademicPlanRecordElementGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? AcademicPlanRecordId { get; set; }

        public Guid? TimeNormId { get; set; }
    }

    public class AcademicPlanRecordElementSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid AcademicPlanRecordId { get; set; }

        public Guid TimeNormId { get; set; }

        public decimal PlanHours { get; set; }

        public decimal FactHours { get; set; }
    }
}
