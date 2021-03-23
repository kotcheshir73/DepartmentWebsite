using Enums;
using System;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class AcademicPlanRecordElementPageViewModel : PageSettingListViewModel<AcademicPlanRecordElementViewModel> { }

    public class AcademicPlanRecordElementViewModel : PageSettingElementViewModel
    {
        public Guid AcademicPlanRecordId { get; set; }

        public Guid TimeNormId { get; set; }

        public Guid DisciplineId { get; set; }

        public string Discipline { get; set; }

        public string KindOfLoadName { get; set; }

        public decimal PlanHours { get; set; }

        public decimal FactHours { get; set; }

        public int Semester { get; set; }
    }
}