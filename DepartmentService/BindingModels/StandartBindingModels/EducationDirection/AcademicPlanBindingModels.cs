using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class AcademicPlanGetBindingModel : PageSettingBinidingModel
	{
		public Guid? Id { get; set; }

        public Guid? AcademicYearId { get; set; }
    }

	public class AcademicPlanRecordBindingModel
	{
		public Guid Id { get; set; }
		
		public Guid AcademicYearId { get; set; }

		public Guid? EducationDirectionId { get; set; }

		[Required(ErrorMessage = "required")]
		public string AcademicLevel { get; set; }
		
		public int? AcademicCourses { get; set; }
	}
}
