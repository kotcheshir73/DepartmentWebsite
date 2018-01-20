using System;

namespace DepartmentService.BindingModels
{
	public class LoadDistributionRecordGetBindingModel : PageSettingBinidingModel
	{
		public Guid? Id { get; set; }

		public Guid? LoadDistributionId { get; set; }

		public int SemesterTime { get; set; }
	}

	public class LoadDistributionRecordRecordBindingModel
	{
		public Guid Id { get; set; }

		public Guid LoadDistributionId { get; set; }

		public Guid AcademicPlanRecordId { get; set; }

		public Guid ContingentId { get; set; }

		public Guid TimeNormId { get; set; }

		public decimal Load { get; set; }
	}
}
