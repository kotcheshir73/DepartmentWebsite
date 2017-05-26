namespace DepartmentService.BindingModels
{
	public class ContingentGetBindingModel
	{
		public long Id { get; set; }
	}

	public class ContingentRecordBindingModel
	{
		public long Id { get; set; }

		public long AcademicYearId { get; set; }

		public long EducationDirectionId { get; set; }

		public int Course { get; set; }

		public int CountStudents { get; set; }

		public int CountSubgroups { get; set; }
	}
}
