using System;

namespace DepartmentService.ViewModels
{
	public class ContingentPageViewModel : PageViewModel<ContingentViewModel> { }

	public class ContingentViewModel
	{
		public Guid Id { get; set; }

		public Guid AcademicYearId { get; set; }

		public Guid EducationDirectionId { get; set; }

		public string EducationDirectionCipher { get; set; }

		public string AcademicYear { get; set; }

        public string ContingentName { get; set; }

        public int Course { get; set; }

        public int CountGroups { get; set; }

        public int CountStudents { get; set; }

		public int CountSubgroups { get; set; }
	}
}
