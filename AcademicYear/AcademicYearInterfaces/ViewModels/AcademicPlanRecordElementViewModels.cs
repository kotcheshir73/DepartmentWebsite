using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class AcademicPlanRecordElementPageViewModel : PageSettingListViewModel<AcademicPlanRecordElementViewModel> { }

    public class AcademicPlanRecordElementViewModel : PageSettingElementViewModel
    {
        [Required]
        [Display(Name = "Запись учебного плана")]
        public Guid AcademicPlanRecordId { get; set; }

        [Required]
        [Display(Name = "Норма времени*")]
        public Guid TimeNormId { get; set; }

        public Guid DisciplineId { get; set; }

        [DisplayName("Дисциплина")]
        public string Discipline { get; set; }

        [DisplayName("Вид нагрузки")]
        public string KindOfLoadName { get; set; }

        [Required]
        [DisplayName("План. часы")]
        [Display(Name = "План. часы*")]
        public decimal PlanHours { get; set; }

        [Required]
        [DisplayName("Факт. часы")]
        [Display(Name = "Факт. часы*")]
        public decimal FactHours { get; set; }

        public int Semester { get; set; }

        public bool InDepartment { get; set; }

        public bool IsFacultative { get; set; }
    }
}