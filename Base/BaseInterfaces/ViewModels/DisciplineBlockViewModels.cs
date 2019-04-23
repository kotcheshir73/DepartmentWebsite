using Tools.ViewModels;

namespace BaseInterfaces.ViewModels
{
    public class DisciplineBlockPageViewModel : PageSettingListViewModel<DisciplineBlockViewModel> { }

	public class DisciplineBlockViewModel : PageSettingElementViewModel
	{
		public string Title { get; set; }
        
        public string DisciplineBlockBlueAsteriskName { get; set; }
        
        public bool DisciplineBlockUseForGrouping { get; set; }
        
        public int DisciplineBlockOrder { get; set; }
    }
}