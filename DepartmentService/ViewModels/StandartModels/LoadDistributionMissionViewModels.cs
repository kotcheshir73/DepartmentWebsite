namespace DepartmentService.ViewModels
{
	public class LoadDistributionMissionPageViewModel : PageViewModel<LoadDistributionMissionViewModel> { }

	public class LoadDistributionMissionViewModel
	{
		public long Id { get; set; }

		public long LoadDistributionRecordId { get; set; }

		public long LecturerId { get; set; }

		public decimal Hours { get; set; }
	}
}
