using System;

namespace DepartmentService.ViewModels
{
	public class AcademicPlanRecordPageViewModel : PageViewModel<AcademicPlanRecordViewModel> { }

	public class AcademicPlanRecordViewModel
	{
		public Guid Id { get; set; }

		public Guid AcademicPlanId { get; set; }

		public Guid DisciplineId { get; set; }

		public string Disciplne { get; set; }

		public string Semester { get; set; }

        public int Zet { get; set; }
    }
}
