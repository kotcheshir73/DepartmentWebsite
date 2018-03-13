using System;

namespace DepartmentService.ViewModels
{
	public class KindOfLoadPageViewModel : PageViewModel<KindOfLoadViewModel> { }

	public class KindOfLoadViewModel
	{
		public Guid Id { get; set; }

		public string KindOfLoadName { get; set; }

        //public string KindOfLoadType { get; set; }

        public string AttributeName { get; set; }
    }
}
