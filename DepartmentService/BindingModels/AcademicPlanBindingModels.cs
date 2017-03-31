using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class AcademicPlanGetBindingModel
	{
		public long Id { get; set; }
	}

	public class AcademicPlanRecordBindingModel
	{
		public long Id { get; set; }

		public long EducationDirectionId { get; set; }

		[Required(ErrorMessage = "required")]
		public string AcademicYear { get; set; }

		[Required(ErrorMessage = "required")]
		public string AcademicLevel { get; set; }
		
		public int AcademicCourses { get; set; }
	}

	public class AcademicPlanRecordGetBindingModel
	{
		public long? Id { get; set; }

		public long? AcademicPlanId { get; set; }
	}

	public class AcademicPlanRecordLoadFromXMLBindingModel
	{
		public long Id { get; set; }

		public string FileName{ get; set; }
	}

	public class AcademicPlanRecordRecordBindingModel
	{
		public long Id { get; set; }

		public long AcademicPlanId { get; set; }

		public long DisciplineId { get; set; }

		public long KindOfLoadId { get; set; }

		public string Semester { get; set; }

		public int Hours { get; set; }
	}
}
