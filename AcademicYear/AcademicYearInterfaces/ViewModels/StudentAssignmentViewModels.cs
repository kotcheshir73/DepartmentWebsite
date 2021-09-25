using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class StudentAssignmentPageViewModel : PageSettingListViewModel<StudentAssignmentViewModel> { }

    public class StudentAssignmentViewModel : PageSettingElementViewModel
    {
        [Required]
        [Display(Name = "Учебный год*")]
        public Guid AcademicYearId { get; set; }

        [Required]
        [Display(Name = "Направление*")]
        public Guid EducationDirectionId { get; set; }

        [DisplayName("Направление")]
        public string EducationDirection { get; set; }

        [Required]
        [Display(Name = "Преподаватель*")]
        public Guid LecturerId { get; set; }

        [DisplayName("Преподаватель")]
        public string Lecturer { get; set; }

        [Required]
        [DisplayName("Количество студентов")]
        [Display(Name = "Количество студентов*")]
        public int CountStudents { get; set; }
    }
}
