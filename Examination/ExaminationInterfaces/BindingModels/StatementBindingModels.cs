using System;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class StatementGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? LecturerId { get; set; }

        public Guid? DisciplineId { get; set; }

        public Guid? StudentGroupId { get; set; }

        public Guid? AcademicYearId { get; set; }

        public Guid? AcademicPlanRecordId { get; set; }
    }

    public class StatementSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid LecturerId { get; set; }

        public Guid DisciplineId { get; set; }

        public Guid StudentGroupId { get; set; }

        public Guid AcademicYearId { get; set; }

        public string TypeOfTest { get; set; }

        public string Semester { get; set; }

        public DateTime? Date { get; set; }

        public bool IsMain { get; set; }
    }
}