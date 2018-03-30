using System.Collections.Generic;

namespace DepartmentService.ViewModels
{
	public class PageViewModel<T>
	{
		public int MaxCount { get; set; }

		public List<T> List { get; set; }
    }

    public class PageViewModel<T, U>
    {
        public int MaxCount { get; set; }

        public List<T> ListFirst { get; set; }

        public List<U> ListSecond { get; set; }
    }
}
