using System;

namespace DepartmentService.BindingModels
{
    public class GraficGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? AcademicPlanRecordId { get; set; }

        public Guid? StudentGroupId { get; set; }

        public Guid? AcademicYearId { get; set; }

        public Guid? LecturerId { get; set; }

    }

    public class GraficSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid AcademicPlanRecordId { get; set; }

        public Guid StudentGroupId { get; set; }

        public string Comment { get; set; }

        public string CommentWishesOfTeacher { get; set; }
    }
}
