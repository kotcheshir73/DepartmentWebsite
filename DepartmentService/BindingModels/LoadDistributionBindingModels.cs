namespace DepartmentService.BindingModels
{
	public class LoadDistributionGetBindingModel
	{
		public long Id { get; set; }
	}

	public class LoadDistributionRecordBindingModel
	{
		public long Id { get; set; }

		public long AcademicYearId { get; set; }
	}

	public class LoadDistributionRecordGetBindingModel
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
	}

	public class LoadDistributionMissionGetBindingModel
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
