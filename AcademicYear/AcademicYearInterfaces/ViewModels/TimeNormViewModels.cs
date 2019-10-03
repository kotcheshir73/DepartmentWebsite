using System;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class TimeNormPageViewModel : PageSettingListViewModel<TimeNormViewModel> { }

    public class TimeNormViewModel : PageSettingElementViewModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid DisciplineBlockId { get; set; }

        public string AcademicYear { get; set; }

        public string DisciplineBlockName { get; set; }

        public string TimeNormName { get; set; }

        public string TimeNormShortName { get; set; }

        public int TimeNormOrder { get; set; }

        public string TimeNormEducationDirectionQualification { get; set; }

        public string KindOfLoadName { get; set; }

        public string KindOfLoadAttributeName { get; set; }

        public string KindOfLoadBlueAsteriskName { get; set; }

        public string KindOfLoadBlueAsteriskAttributeName { get; set; }

        public string KindOfLoadBlueAsteriskPracticName { get; set; }

        public string KindOfLoadType { get; set; }

        public decimal? Hours { get; set; }

        public decimal? NumKoef { get; set; }

        public string TimeNormKoef { get; set; }

        public bool UseInLearningProgress { get; set; }

        public bool UseInSite { get; set; }
    }
}