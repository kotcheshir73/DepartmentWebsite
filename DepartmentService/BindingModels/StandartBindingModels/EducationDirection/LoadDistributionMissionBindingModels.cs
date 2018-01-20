using System;

namespace DepartmentService.BindingModels
{
	public class LoadDistributionMissionGetBindingModel : PageSettingBinidingModel
	{
		public Guid? Id { get; set; }

		public Guid? LoadDistributionRecordId { get; set; }

		public Guid? LecturerId { get; set; }
	}

	public class LoadDistributionMissionRecordBindingModel
	{
		public Guid Id { get; set; }

		public Guid LoadDistributionRecordId { get; set; }

		public Guid LecturerId { get; set; }

		public decimal Hours { get; set; }
	}
}
