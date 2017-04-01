namespace DepartmentService.ViewModels
{
    public class StudentGroupViewModel
    {
        public long Id { get; set; }

        public long EducationDirectionId { get; set; }

        public string EducationDirectionCipher { get; set; }
        
        public string GroupName { get; set; }

        public int Kurs { get; set; }

        public int CountStudents { get; set; }

        public string StewardId { get; set; }

        public long? CuratorId { get; set; }

        public string Steward { get; set; }

        public string Curator { get; set; }
	}

	public class ContingentViewModel
	{
		public long Id { get; set; }

		public long AcademicYearId { get; set; }

		public long StudentGroupId { get; set; }

		public string AcademicYear { get; set; }

		public string StudentGroupName { get; set; }

		public int CountStudents { get; set; }

		public int CountSubgroups { get; set; }
	}
}
