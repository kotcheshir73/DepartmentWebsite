namespace DepartmentService.BindingModels
{
	public class LoadDistributionMissionGetBindingModel : PageSettingBinidingModel
	{
		public long? Id { get; set; }

		public long? LoadDistributionRecordId { get; set; }

		public long? LecturerId { get; set; }
	}

	public class LoadDistributionMissionRecordBindingModel
	{
		public long Id { get; set; }

		public long LoadDistributionRecordId { get; set; }

		public long LecturerId { get; set; }

		public decimal Hours { get; set; }
	}
}
