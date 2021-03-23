using System;
using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
    public class AcademicPlanRecordElementGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? AcademicPlanRecordId { get; set; }

        public Guid? AcademicYearId { get; set; }

        public Guid? TimeNormId { get; set; }

        public Guid? ContingentId { get; set; }

        public bool? TimeNormUseInSite { get; set; }

        public bool? AcademicPlanRecordIsSelected { get; set; }
    }

    public class AcademicPlanRecordElementSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid AcademicPlanRecordId { get; set; }

        public Guid TimeNormId { get; set; }

        public decimal PlanHours { get; set; }

        public decimal FactHours { get; set; }
    }
}