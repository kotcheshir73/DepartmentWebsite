namespace DepartmentService.ViewModels
{
	public class LoadDistributionViewModel
	{
		public long Id { get; set; }

		public long AcademicYearId { get; set; }

		public string AcademicYear { get; set; }
	}

	public class LoadDistributionRecordViewModel
	{
		public long Id { get; set; }

		public long LoadDistributionId { get; set; }

		public string LoadDistributionAcademicYear { get; set; }

		public long AcademicPlanRecordId { get; set; }

		public string EducationDirectionCipher { get; set; }

		public  string Disciplne { get; set; }

		public string DisciplineBlockTitle { get; set; }

		public long ContingentId { get; set; }

		public string StudentGroupName { get; set; }

		public long TimeNormId { get; set; }

	//	public TimeNormViewModel TimeNormViewModel { get; set; }

		public int SemesterNumber { get; set; }

		public decimal Load { get; set; }
	}

	public class LoadDistributionMissionViewModel
	{
		public long Id { get; set; }

		public long LoadDistributionRecordId { get; set; }

		public long LecturerId { get; set; }

		public decimal Hours { get; set; }
	}
}
