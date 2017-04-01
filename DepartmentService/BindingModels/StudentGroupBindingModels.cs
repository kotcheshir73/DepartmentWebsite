using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class StudentGroupGetBindingModel
    {
        public long Id { get; set; }

		public string GroupName { get; set; }
	}

	public class StudentGroupRecordBindingModel
    {
        public long Id { get; set; }

        public long EducationDirectionId { get; set; }

        [Required(ErrorMessage = "required")]
        public string GroupName { get; set; }
        
        public int Kurs { get; set; }

        public string StewardId { get; set; }

        public long? CuratorId { get; set; }
	}

	public class ContingentGetBindingModel
	{
		public long Id { get; set; }
	}

	public class ContingentRecordBindingModel
	{
		public long Id { get; set; }

		public long AcademicYearId { get; set; }

		public long StudentGroupId { get; set; }

		[Required(ErrorMessage = "required")]
		public string GroupName { get; set; }

		public int CountStudents { get; set; }

		public int CountSubgroups { get; set; }
	}
}
