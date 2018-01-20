using System;

namespace DepartmentService.ViewModels
{
	public class LoadDistributionMissionPageViewModel : PageViewModel<LoadDistributionMissionViewModel> { }

	public class LoadDistributionMissionViewModel
	{
		public Guid Id { get; set; }

		public Guid LoadDistributionRecordId { get; set; }

		public Guid LecturerId { get; set; }

		public decimal Hours { get; set; }
	}
}
