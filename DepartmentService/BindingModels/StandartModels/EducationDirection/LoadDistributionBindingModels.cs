namespace DepartmentService.BindingModels
{
	public class LoadDistributionGetBindingModel : PageSettingBinidingModel
	{
		public long? Id { get; set; }
	}

	public class LoadDistributionRecordBindingModel
	{
		public long Id { get; set; }

		public long AcademicYearId { get; set; }
	}
}
