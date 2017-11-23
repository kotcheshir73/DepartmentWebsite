using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class SeasonDatesGetBindingModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

		public int? PageNumber { get; set; }

		public int? PageSize { get; set; }
	}

    public class SeasonDatesRecordBindingModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "required")]
        public DateTime DateBeginSemester { get; set; }

        [Required(ErrorMessage = "required")]
        public DateTime DateEndSemester { get; set; }

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
