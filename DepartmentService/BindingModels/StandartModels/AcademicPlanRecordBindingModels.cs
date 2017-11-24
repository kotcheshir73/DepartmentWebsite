namespace DepartmentService.BindingModels
{
	public class AcademicPlanRecordGetBindingModel : PageSettingBinidingModel
	{
		public long? Id { get; set; }

		public long? AcademicPlanId { get; set; }
	}

	public class AcademicPlanRecordRecordBindingModel
	{
		public long Id { get; set; }

		public long AcademicPlanId { get; set; }

		public long DisciplineId { get; set; }

		public long KindOfLoadId { get; set; }

		public string Semester { get; set; }

		public int Hours { get; set; }
	}
}
