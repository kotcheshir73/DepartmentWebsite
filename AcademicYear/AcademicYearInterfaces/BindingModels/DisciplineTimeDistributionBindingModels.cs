using System;
using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
    public class DisciplineTimeDistributionGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? AcademicPlanRecordId { get; set; }

        public Guid? StudentGroupId { get; set; }

        public Guid? AcademicYearId { get; set; }

        public Guid? LecturerId { get; set; }

    }

    public class DisciplineTimeDistributionSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid AcademicPlanRecordId { get; set; }

        public Guid StudentGroupId { get; set; }

        public string Comment { get; set; }

        public string CommentWishesOfTeacher { get; set; }
    }
}