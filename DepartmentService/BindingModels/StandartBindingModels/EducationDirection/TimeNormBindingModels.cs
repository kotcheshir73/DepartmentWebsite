using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class TimeNormGetBindingModel : PageSettingBinidingModel
	{
		public Guid? Id { get; set; }

        public Guid? AcademicYearId { get; set; }

        public Guid? AcademicPlanRecordId { get; set; }

        public Guid? DisciplineBlockId { get; set; }
    }

	public class TimeNormRecordBindingModel
	{
		public Guid Id { get; set; }

        public Guid AcademicYearId { get; set; }

        public Guid DisciplineBlockId { get; set; }

        [Required(ErrorMessage = "required")]
		public string TimeNormName { get; set; }

        [Required(ErrorMessage = "required")]
        public string TimeNormShortName { get; set; }

        [Required(ErrorMessage = "required")]
        public int TimeNormOrder { get; set; }

        public string TimeNormAcademicLevel { get; set; }

        [Required(ErrorMessage = "required")]
        public string KindOfLoadName { get; set; }

        public string KindOfLoadAttributeName { get; set; }

        public string KindOfLoadBlueAsteriskName { get; set; }

        public string KindOfLoadBlueAsteriskAttributeName { get; set; }

        public string KindOfLoadBlueAsteriskPracticName { get; set; }

        [Required(ErrorMessage = "required")]
        public string KindOfLoadType { get; set; }

		public decimal? Hours { get; set; }

        public decimal? NumKoef { get; set; }

        [Required(ErrorMessage = "required")]
        public string TimeNormKoef { get; set; }
    }
}
