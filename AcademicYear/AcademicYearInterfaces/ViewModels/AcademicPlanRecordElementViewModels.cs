using Enums;
using System;
using System.ComponentModel;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class AcademicPlanRecordElementPageViewModel : PageSettingListViewModel<AcademicPlanRecordElementViewModel> { }

    public class AcademicPlanRecordElementViewModel : PageSettingElementViewModel
    {
        public Guid AcademicPlanRecordId { get; set; }

        public Guid TimeNormId { get; set; }

        public Guid DisciplineId { get; set; }

        [DisplayName("Дисциплина")]
        public string Discipline { get; set; }

        [DisplayName("Вид нагрузки")]
        public string KindOfLoadName { get; set; }

        [DisplayName("План. часы")]
        public decimal PlanHours { get; set; }

        [DisplayName("Факт. часы")]
        public decimal FactHours { get; set; }

        public int Semester { get; set; }

        public bool InDepartment { get; set; }

        public bool IsFacultative { get; set; }
    }
}