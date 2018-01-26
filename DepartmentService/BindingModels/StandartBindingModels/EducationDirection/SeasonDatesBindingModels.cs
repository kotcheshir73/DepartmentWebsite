using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class SeasonDatesGetBindingModel : PageSettingBinidingModel
	{
        public Guid? Id { get; set; }

        public string Title { get; set; }
	}

    public class SeasonDatesRecordBindingModel
    {
        public Guid Id { get; set; }

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
