using System;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class DisciplineTimeDistributionClassroomPageViewModel : PageSettingListViewModel<DisciplineTimeDistributionClassroomViewModel> { }

    public class DisciplineTimeDistributionClassroomViewModel : PageSettingElementViewModel
    {
        public Guid DisciplineTimeDistributionId { get; set; }

        public Guid TimeNormId { get; set; }

        public string DisciplineTimeDistribution { get; set; }

        public string TimeNorm { get; set; }

        public string ClassroomDescription { get; set; }
    }
}