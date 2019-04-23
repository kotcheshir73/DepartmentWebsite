using System;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
	public class ContingentPageViewModel : PageSettingListViewModel<ContingentViewModel> { }

	public class ContingentViewModel : PageSettingElementViewModel
	{
		public Guid AcademicYearId { get; set; }

		public Guid EducationDirectionId { get; set; }

		public string EducationDirectionShortName { get; set; }

		public string AcademicYear { get; set; }

        public string ContingentName { get; set; }

        public int Course { get; set; }

        public int CountGroups { get; set; }

        public int CountStudents { get; set; }

		public int CountSubgroups { get; set; }
	}
}