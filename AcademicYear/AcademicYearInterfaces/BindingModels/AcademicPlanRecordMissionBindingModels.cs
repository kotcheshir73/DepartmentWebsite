using System;
using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
    public class AcademicPlanRecordMissionGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? AcademicPlanRecordElementId { get; set; }

        public Guid? DisciplineId { get; set; }

        public Guid? LecturerId { get; set; }

        public Guid? AcademicYearId { get; set; }
    }

    public class AcademicPlanRecordMissionSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid AcademicPlanRecordElementId { get; set; }

        public Guid LecturerId { get; set; }

        public decimal Hours { get; set; }
    }
}