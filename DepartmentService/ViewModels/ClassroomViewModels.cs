using System.Collections.Generic;

namespace DepartmentService.ViewModels
{
	public class ClassroomPageViewModel
	{
		public int MaxCount { get; set; }

		public List<ClassroomViewModel> List { get; set; }
	}

	public class ClassroomViewModel
    {
        public string Id { get; set; }

        public string ClassroomType { get; set; }

        public int Capacity { get; set; }
    }
}
