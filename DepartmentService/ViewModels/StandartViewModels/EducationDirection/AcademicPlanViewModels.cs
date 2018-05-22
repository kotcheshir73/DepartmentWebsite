using System;

namespace DepartmentService.ViewModels
{
	public class AcademicPlanPageViewModel : PageViewModel<AcademicPlanViewModel> { }

	public class AcademicPlanViewModel
	{
		public Guid Id { get; set; }

		public Guid AcademicYearId { get; set; }

		public Guid? EducationDirectionId { get; set; }

		public string AcademicYear { get; set; }

		public string EducationDirection { get; set; }

		public string AcademicLevel { get; set; }

		public string AcademicCoursesStrings { get; set; }

		public int? AcademicCourses { get; set; }
	}
}
