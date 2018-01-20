using System;

namespace DepartmentService.ViewModels
{
	public class LoadDistributionRecordPageViewModel : PageViewModel<LoadDistributionRecordViewModel> { }

	public class LoadDistributionRecordViewModel
	{
		public Guid Id { get; set; }

		public Guid LoadDistributionId { get; set; }

		public Guid AcademicPlanRecordId { get; set; }

		public Guid ContingentId { get; set; }

		public Guid TimeNormId { get; set; }

		public string LoadDistributionAcademicYear { get; set; }

		public string EducationDirectionCipher { get; set; }

		public string Disciplne { get; set; }

		public string DisciplineBlockTitle { get; set; }

		public int SemesterNumber { get; set; }

		public decimal Load { get; set; }
	}
}
