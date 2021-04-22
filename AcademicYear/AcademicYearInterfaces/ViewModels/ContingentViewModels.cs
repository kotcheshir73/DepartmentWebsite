using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class ContingentPageViewModel : PageSettingListViewModel<ContingentViewModel> { }

    public class ContingentViewModel : PageSettingElementViewModel
    {
        [Required]
        [Display(Name = "Учебный год*")]
        public Guid AcademicYearId { get; set; }

        [Required]
        [Display(Name = "Направление*")]
        public Guid EducationDirectionId { get; set; }

        [DisplayName("Направление")]
        public string EducationDirectionShortName { get; set; }

        public string AcademicYear { get; set; }

        [Required]
        [DisplayName("Наименование")]
        [Display(Name = "Наименование*")]
        public string ContingentName { get; set; }

        [Required]
        [DisplayName("Курс")]
        [Display(Name = "Курс*")]
        public int Course { get; set; }

        [Required]
        [DisplayName("Количество групп")]
        [Display(Name = "Количество групп*")]
        public int CountGroups { get; set; }

        [Required]
        [DisplayName("Количество студентов")]
        [Display(Name = "Количество студентов*")]
        public int CountStudents { get; set; }

        [Required]
        [DisplayName("Количество подгрупп")]
        [Display(Name = "Количество подгрупп*")]
        public int CountSubgroups { get; set; }
    }
}