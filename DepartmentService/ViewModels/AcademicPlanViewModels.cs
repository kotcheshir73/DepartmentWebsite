namespace DepartmentService.ViewModels
{
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

	public class AcademicPlanRecordViewModel
	{
		public long Id { get; set; }

		public long AcademicPlanId { get; set; }

		public long DisciplineId { get; set; }

		public string Disciplne { get; set; }

		public long KindOfLoadId { get; set; }

		public string KindOfLoad { get; set; }

		public string Semester { get; set; }

		public int Hours { get; set; }
	}

	public class AcademicYearViewModel
	{
		public long Id { get; set; }

		public string Title { get; set; }
	}
}
