using System;
using Tools.ViewModels;

namespace BaseInterfaces.ViewModels
{
	public class DisciplinePageViewModel : PageSettingListViewModel<DisciplineViewModel> { }

	public class DisciplineViewModel : PageSettingElementViewModel
	{
		public Guid DisciplineBlockId { get; set; }

        public Guid DisciplineParentId { get; set; }

        public bool IsParent { get; set; }

        public string DisciplineName { get; set; }

        public string DisciplineShortName { get; set; }

        public string DisciplineBlockTitle { get; set; }

        public string DisciplineBlueAsteriskName { get; set; }

        public string DisciplineDescription { get; set; }
    }
}