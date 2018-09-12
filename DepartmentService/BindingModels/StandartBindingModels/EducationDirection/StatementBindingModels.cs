using System;

namespace DepartmentService.BindingModels
{
    public class StatementGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? LecturerId { get; set; }

        public Guid? AcademicPlanRecordId { get; set; }

        public Guid? StudentGroupId { get; set; }

        public Guid? AcademicYearId { get; set; }
    }

    public class StatementSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid LecturerId { get; set; }

        public Guid AcademicPlanRecordId { get; set; }

        public Guid StudentGroupId { get; set; }

        public string Course { get; set; }

        public string TypeOfTest { get; set; }

        public string Semester { get; set; }

        public DateTime? Date { get; set; }
    }
}
