namespace DepartmentService.BindingModels
{
	public class LoadDistributionRecordGetBindingModel : PageSettingBinidingModel
	{
		public long? Id { get; set; }

		public long? LoadDistributionId { get; set; }

		public int SemesterTime { get; set; }
	}

	public class LoadDistributionRecordRecordBindingModel
	{
		public long Id { get; set; }

		public long LoadDistributionId { get; set; }

		public long AcademicPlanRecordId { get; set; }

		public long ContingentId { get; set; }

		public long TimeNormId { get; set; }

		public decimal Load { get; set; }
	}
}
