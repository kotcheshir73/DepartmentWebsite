using System.Collections.Generic;

namespace Tools.ViewModels
{
	public class PageSettingListViewModel<T>
	{
		public int MaxCount { get; set; }

		public List<T> List { get; set; }
    }

    public class PageSettingListViewModel<T, U>
    {
        public int MaxCount { get; set; }

        public List<T> ListFirst { get; set; }

        public List<U> ListSecond { get; set; }
    }
}