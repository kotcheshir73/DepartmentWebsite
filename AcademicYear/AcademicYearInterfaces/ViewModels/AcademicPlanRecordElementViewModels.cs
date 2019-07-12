using System;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class AcademicPlanRecordElementPageViewModel : PageSettingListViewModel<AcademicPlanRecordElementViewModel> { }

    public class AcademicPlanRecordElementViewModel : PageSettingElementViewModel
    {
        public Guid AcademicPlanRecordId { get; set; }

        public Guid TimeNormId { get; set; }

        public string Disciplne { get; set; }

        public string KindOfLoadName { get; set; }

        public decimal PlanHours { get; set; }

        public decimal FactHours { get; set; }
    }
}