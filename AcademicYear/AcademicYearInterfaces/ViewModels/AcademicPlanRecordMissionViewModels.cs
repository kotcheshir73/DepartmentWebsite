using System;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class AcademicPlanRecordMissionPageViewModel : PageSettingListViewModel<AcademicPlanRecordMissionViewModel> { }

    public class AcademicPlanRecordMissionViewModel : PageSettingElementViewModel
    {
        public Guid AcademicPlanRecordElementId { get; set; }

        public Guid LecturerId { get; set; }

        public Guid DisciplineId { get; set; }

        public string AcademicPlanRecordElementTitle { get; set; }

        public string LecturerName { get; set; }

        public string DisciplineTitle { get; set; }

        public decimal Hours { get; set; }
    }
}