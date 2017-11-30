﻿namespace DepartmentService.ViewModels
{
	public class ClassroomPageViewModel : PageViewModel<ClassroomViewModel> { }

	public class ClassroomViewModel
	{
		public string Id { get; set; }

		public string ClassroomType { get; set; }

		public int Capacity { get; set; }
	}
}