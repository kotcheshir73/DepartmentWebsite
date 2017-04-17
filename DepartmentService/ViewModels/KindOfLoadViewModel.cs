namespace DepartmentService.ViewModels
{
	public class KindOfLoadViewModel
	{
		public long Id { get; set; }

		public string KindOfLoadName { get; set; }
		
		public string KindOfLoadType { get; set; }
	}

	public class TimeNormViewModel
	{
		public long Id { get; set; }

		public long KindOfLoadId { get; set; }

		public string Title { get; set; }

		public string KindOfLoadName { get; set; }

		public string Formula { get; set; }

		public decimal Hours { get; set; }
	}
}
