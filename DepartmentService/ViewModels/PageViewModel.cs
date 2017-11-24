using System.Collections.Generic;

namespace DepartmentService.ViewModels
{
	public class PageViewModel<T>
	{
		public int MaxCount { get; set; }

		public List<T> List { get; set; }
	}
}
