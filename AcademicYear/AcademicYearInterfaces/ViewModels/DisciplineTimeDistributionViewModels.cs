using System;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class DisciplineTimeDistributionPageViewModel : PageSettingListViewModel<DisciplineTimeDistributionViewModel> { }

    public class DisciplineTimeDistributionViewModel : PageSettingElementViewModel
    {
        public Guid AcademicPlanRecordId { get; set; }

        public Guid StudentGroupId { get; set; }

        public string Comment { get; set; }

        public string CommentWishesOfTeacher { get; set; }
        
        public string StudentGroupName { get; set; }

        public string DisciplineName { get; set; }

        public string Semester { get; set; }
    }
}