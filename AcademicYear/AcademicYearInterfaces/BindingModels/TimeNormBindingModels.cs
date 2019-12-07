using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
	public class TimeNormGetBindingModel : PageSettingGetBinidingModel
	{
        public Guid? AcademicYearId { get; set; }

        public Guid? AcademicPlanRecordId { get; set; }

        public Guid? DisciplineBlockId { get; set; }
    }

	public class TimeNormSetBindingModel : PageSettingSetBinidingModel
	{
        public Guid AcademicYearId { get; set; }

        public Guid DisciplineBlockId { get; set; }

        [Required(ErrorMessage = "required")]
		public string TimeNormName { get; set; }

        [Required(ErrorMessage = "required")]
        public string TimeNormShortName { get; set; }

        [Required(ErrorMessage = "required")]
        public int TimeNormOrder { get; set; }

        public string TimeNormEducationDirectionQualification { get; set; }

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

        public bool UseInLearningProgress { get; set; }

        /// <summary>
        /// Код вида работ в справочнике видов работ в новой версии планов, чтобы потом искать работу в строках плана
        /// </summary>
        public string KindOfLoadBlueAsteriskCode { get; set; }

        /// <summary>
        /// Код вида практики в справочнике видов практик в новой версии планов, чтобы потом искать практику в строках плана
        /// </summary>
        public string KindOfLoadBlueAsteriskPracticCode { get; set; }
    }
}