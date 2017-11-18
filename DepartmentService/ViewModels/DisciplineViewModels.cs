using System.Collections.Generic;

namespace DepartmentService.ViewModels
{
	public class DisciplineBlockPageViewModel
	{
		public int MaxCount { get; set; }

		public List<DisciplineBlockViewModel> List { get; set; }
	}

	public class DisciplineBlockViewModel
	{
		public long Id { get; set; }

		public string Title { get; set; }
	}

	public class DisciplineViewModel
	{
		public long Id { get; set; }

		public long DisciplineBlockId { get; set; }

		public string DisciplineName { get; set; }

		public string DisciplineBlockTitle { get; set; }
	}
}
