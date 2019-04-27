using System;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class IndividualPlanPageViewModel : PageSettingListViewModel<IndividualPlanViewModel> { }

    public class IndividualPlanViewModel : PageSettingElementViewModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid LecturerId { get; set; }

        public string AcademicYearsTitle { get; set; }

        public string LecturerName { get; set; }
    }
}