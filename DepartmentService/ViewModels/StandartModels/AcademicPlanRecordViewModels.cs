namespace DepartmentService.ViewModels
{
	public class AcademicPlanRecordPageViewModel : PageViewModel<AcademicPlanRecordViewModel> { }

	public class AcademicPlanRecordViewModel
	{
		public long Id { get; set; }

		public long AcademicPlanId { get; set; }

		public long DisciplineId { get; set; }

		public string Disciplne { get; set; }

		public long KindOfLoadId { get; set; }

		public string KindOfLoad { get; set; }

		public string Semester { get; set; }

		public int Hours { get; set; }
	}
}
