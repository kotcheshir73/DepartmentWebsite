namespace DepartmentService.ViewModels
{
	public class AcademicPlanPageViewModel : PageViewModel<AcademicPlanViewModel> { }

	public class AcademicPlanViewModel
	{
		public long Id { get; set; }

		public long EducationDirectionId { get; set; }

		public long AcademicYearId { get; set; }

		public string EducationDirection { get; set; }

		public string AcademicYear { get; set; }

		public string AcademicLevel { get; set; }

		public string AcademicCoursesStrings { get; set; }

		public int AcademicCourses { get; set; }
	}
}
