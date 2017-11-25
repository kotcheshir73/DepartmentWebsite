namespace DepartmentService.ViewModels
{
	public class LoadDistributionRecordPageViewModel : PageViewModel<LoadDistributionRecordViewModel> { }

	public class LoadDistributionRecordViewModel
	{
		public long Id { get; set; }

		public long LoadDistributionId { get; set; }

		public string LoadDistributionAcademicYear { get; set; }

		public long AcademicPlanRecordId { get; set; }

		public string EducationDirectionCipher { get; set; }

		public string Disciplne { get; set; }

		public string DisciplineBlockTitle { get; set; }

		public long ContingentId { get; set; }

		public long TimeNormId { get; set; }

		public int SemesterNumber { get; set; }

		public decimal Load { get; set; }
	}
}
