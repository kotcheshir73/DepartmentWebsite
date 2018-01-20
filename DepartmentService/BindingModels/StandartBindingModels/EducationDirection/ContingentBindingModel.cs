using System;

namespace DepartmentService.BindingModels
{
	public class ContingentGetBindingModel : PageSettingBinidingModel
	{
		public Guid? Id { get; set; }
	}

	public class ContingentRecordBindingModel
	{
		public Guid Id { get; set; }

		public Guid AcademicYearId { get; set; }

		public Guid EducationDirectionId { get; set; }

		public int Course { get; set; }

		public int CountStudents { get; set; }

		public int CountSubgroups { get; set; }
	}
}
