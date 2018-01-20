using System;

namespace DepartmentService.BindingModels
{
	public class AcademicPlanRecordGetBindingModel : PageSettingBinidingModel
	{
		public Guid? Id { get; set; }

		public Guid? AcademicPlanId { get; set; }
	}

	public class AcademicPlanRecordRecordBindingModel
	{
		public Guid Id { get; set; }

		public Guid AcademicPlanId { get; set; }

		public Guid DisciplineId { get; set; }

		public Guid KindOfLoadId { get; set; }

		public string Semester { get; set; }

		public int Hours { get; set; }
	}
}
