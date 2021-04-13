using Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class AcademicPlanPageViewModel : PageSettingListViewModel<AcademicPlanViewModel> { }

    public class AcademicPlanViewModel : PageSettingElementViewModel
    {
        [Required]
        [Display(Name = "Учебный год*")]
        public Guid AcademicYearId { get; set; }

        [Display(Name = "Направление")]
        public Guid? EducationDirectionId { get; set; }

        public string AcademicYear { get; set; }

        [DisplayName("Направление")]
        public string EducationDirection { get; set; }

        [DisplayName("Курсы")]
        public string AcademicCoursesStrings { get; set; }

        [Display(Name = "Курсы")]
        public int? AcademicCourses { get; set; }

        public override string ToString()
        {
            return string.Format("{0}. {1} курсы", EducationDirection, AcademicCoursesStrings);
        }

        public AcademicCourse academicCourse()
        {
            return (AcademicCourse)Enum.ToObject(typeof(AcademicCourse), AcademicCourses);
        }
    }
}