namespace DepartmentService.ViewModels
{
	public class DisciplinePageViewModel : PageViewModel<DisciplineViewModel> { }

	public class DisciplineViewModel
	{
		public long Id { get; set; }

		public long DisciplineBlockId { get; set; }

		public string DisciplineName { get; set; }

        public string DisciplineShortName { get; set; }

        public string DisciplineBlockTitle { get; set; }
	}
}
