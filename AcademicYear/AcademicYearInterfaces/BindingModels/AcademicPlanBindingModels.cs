using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
	public class AcademicPlanGetBindingModel : PageSettingGetBinidingModel
	{
        public Guid? AcademicYearId { get; set; }
    }

	public class AcademicPlanSetBindingModel : PageSettingSetBinidingModel
    {
		public Guid AcademicYearId { get; set; }

		public Guid? EducationDirectionId { get; set; }

		[Required(ErrorMessage = "required")]
		public string AcademicLevel { get; set; }
		
		public int? AcademicCourses { get; set; }
	}
}