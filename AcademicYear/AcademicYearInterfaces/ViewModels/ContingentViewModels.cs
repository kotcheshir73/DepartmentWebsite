using System;
using System.ComponentModel;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class ContingentPageViewModel : PageSettingListViewModel<ContingentViewModel> { }

    public class ContingentViewModel : PageSettingElementViewModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid EducationDirectionId { get; set; }

        [DisplayName("Направление")]
        public string EducationDirectionShortName { get; set; }

        public string AcademicYear { get; set; }

        [DisplayName("Наименование")]
        public string ContingentName { get; set; }

        [DisplayName("Курс")]
        public int Course { get; set; }

        [DisplayName("Количество групп")]
        public int CountGroups { get; set; }

        [DisplayName("Количество студентов")]
        public int CountStudents { get; set; }

        [DisplayName("Количество подгрупп")]
        public int CountSubgroups { get; set; }
    }
}