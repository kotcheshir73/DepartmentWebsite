using System;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
	public class AcademicPlanPageViewModel : PageSettingListViewModel<AcademicPlanViewModel> { }

	public class AcademicPlanViewModel : PageSettingElementViewModel
	{
		public Guid AcademicYearId { get; set; }

		public Guid? EducationDirectionId { get; set; }

		public string AcademicYear { get; set; }

		public string EducationDirection { get; set; }

		public string AcademicCoursesStrings { get; set; }

		public int? AcademicCourses { get; set; }
	}
}