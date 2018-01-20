using System;

namespace DepartmentService.BindingModels
{
	public class LoadDistributionGetBindingModel : PageSettingBinidingModel
	{
		public Guid? Id { get; set; }
	}

	public class LoadDistributionRecordBindingModel
	{
		public Guid Id { get; set; }

		public Guid AcademicYearId { get; set; }
	}
}
