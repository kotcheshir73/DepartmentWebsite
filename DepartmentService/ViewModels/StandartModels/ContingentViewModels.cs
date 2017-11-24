namespace DepartmentService.ViewModels
{
	public class ContingentPageViewModel : PageViewModel<ContingentViewModel> { }

	public class ContingentViewModel
	{
		public long Id { get; set; }

		public long AcademicYearId { get; set; }

		public long EducationDirectionId { get; set; }

		public string EducationDirectionCipher { get; set; }

		public string AcademicYear { get; set; }

		public int Course { get; set; }

		public int CountStudents { get; set; }

		public int CountSubgroups { get; set; }
	}
}
