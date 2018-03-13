using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class TimeNormGetBindingModel : PageSettingBinidingModel
	{
		public Guid? Id { get; set; }

        public Guid? AcademicYearId { get; set; }
    }

	public class TimeNormRecordBindingModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "required")]
		public string Title { get; set; }

		public Guid KindOfLoadId { get; set; }

        public Guid AcademicYearId { get; set; }

        //public string Formula { get; set; }

		public decimal? Hours { get; set; }

        public decimal? NumKoef { get; set; }

        [Required(ErrorMessage = "required")]
        public string KindOfLoadType { get; set; }

        [Required(ErrorMessage = "required")]
        public string TimeNormKoef { get; set; }
    }
}
