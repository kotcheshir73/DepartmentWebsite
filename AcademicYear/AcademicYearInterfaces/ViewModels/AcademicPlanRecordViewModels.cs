using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class AcademicPlanRecordPageViewModel : PageSettingListViewModel<AcademicPlanRecordViewModel> { }

    public class AcademicPlanRecordViewModel : PageSettingElementViewModel
    {
        [Required]
        [Display(Name = "Учебный план*")]
        public Guid AcademicPlanId { get; set; }

        [Required]
        [Display(Name = "Дисциплина*")]
        public Guid DisciplineId { get; set; }

        [Display(Name = "Контингент")]
        public Guid? ContingentId { get; set; }

        [DisplayName("Дисциплина")]
        public string Discipline { get; set; }

        [DisplayName("Кафедр.")]
        public bool InDepartment { get; set; }

        [DisplayName("Семестр")]
        [Display(Name = "Семестр")]
        public string Semester { get; set; }

        [DisplayName("Контингент")]
        public string ContingentGroup { get; set; }

        [Required]
        [DisplayName("Зет")]
        [Display(Name = "Зет*")]
        public int Zet { get; set; }

        public Guid? AcademicPlanRecordParentId { get; set; }

        [DisplayName("Род.")]
        /// <summary>
        /// Является родительской для дисциплин по выбору
        /// </summary>
        public bool IsParent { get; set; }

        [DisplayName("Дисц. по выб.")]
        /// <summary>
        /// Является дисциплиной по выбору
        /// </summary>
        public bool IsChild { get; set; }

        [DisplayName("Факульт.")]
        /// <summary>
        /// Является факультативной дисциплиной
        /// </summary>
        public bool IsFacultative { get; set; }

        [DisplayName("Есть в расп.")]
        /// <summary>
        /// Является преподаваемой дисциплиной по учебному плану (есть группы, которые проходят эту дисциплину в этом учебном году)
        /// </summary>
        public bool IsActiveSemester { get; set; }

        [DisplayName("Уч. в расчете")]
        [Display(Name = "Участвует в расчете")]
        /// <summary>
        /// Участвует в расчетах нагрузки
        /// </summary>
        public bool IsUseInWorkload { get; set; }
    }
}