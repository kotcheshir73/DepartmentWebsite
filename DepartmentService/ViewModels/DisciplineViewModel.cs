namespace DepartmentService.ViewModels
{
	public class DisciplineViewModel
	{
		public long Id { get; set; }

		public long DisciplineBlockId { get; set; }

		public string DisciplineName { get; set; }

		public string DisciplineBlockTitle { get; set; }
	}

	public class DisciplineBlockViewModel
	{
		public long Id { get; set; }

		public string Title { get; set; }
	}
}
