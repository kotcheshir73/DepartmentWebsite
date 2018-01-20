using System;

namespace DepartmentService.ViewModels
{
	public class ClassroomPageViewModel : PageViewModel<ClassroomViewModel> { }

	public class ClassroomViewModel
	{
		public Guid Id { get; set; }

        public string Number { get; set; }

		public string ClassroomType { get; set; }

		public int Capacity { get; set; }
	}
}
