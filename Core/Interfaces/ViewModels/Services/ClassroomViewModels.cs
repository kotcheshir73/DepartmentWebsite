namespace Interfaces.ViewModels
{
    public class ClassroomPageViewModel : PageSettingListViewModel<ClassroomViewModel> { }

	public class ClassroomViewModel : PageSettingElementViewModel
	{
        public string Number { get; set; }

		public string ClassroomType { get; set; }

		public int Capacity { get; set; }

        public bool NotUseInSchedule { get; set; }
    }
}