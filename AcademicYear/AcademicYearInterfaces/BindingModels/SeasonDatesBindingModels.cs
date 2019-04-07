using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
    public class SeasonDatesGetBindingModel : PageSettingGetBinidingModel
	{
        public string Title { get; set; }

        public Guid? AcademicYearId { get; set; }
    }

    public class SeasonDatesSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid AcademicYearId { get; set; }

        [Required(ErrorMessage = "required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "required")]
        public DateTime DateBeginFirstHalfSemester { get; set; }

        [Required(ErrorMessage = "required")]
        public DateTime DateEndFirstHalfSemester { get; set; }

        [Required(ErrorMessage = "required")]
        public DateTime DateBeginSecondHalfSemester { get; set; }

        [Required(ErrorMessage = "required")]
        public DateTime DateEndSecondHalfSemester { get; set; }

        [Required(ErrorMessage = "required")]
        public DateTime DateBeginOffset { get; set; }

        [Required(ErrorMessage = "required")]
        public DateTime DateEndOffset { get; set; }

        [Required(ErrorMessage = "required")]
        public DateTime DateBeginExamination { get; set; }

        [Required(ErrorMessage = "required")]
        public DateTime DateEndExamination { get; set; }

        public DateTime? DateBeginPractice { get; set; }

        public DateTime? DateEndPractice { get; set; }
    }
}