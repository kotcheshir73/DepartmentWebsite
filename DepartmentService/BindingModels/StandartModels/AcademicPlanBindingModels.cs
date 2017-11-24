using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class AcademicPlanGetBindingModel : PageSettingBinidingModel
	{
		public long? Id { get; set; }
	}

	public class AcademicPlanRecordBindingModel
	{
		public long Id { get; set; }

		public long EducationDirectionId { get; set; }
		
		public long AcademicYearId { get; set; }

		[Required(ErrorMessage = "required")]
		public string AcademicLevel { get; set; }
		
		public int AcademicCourses { get; set; }
	}
}
