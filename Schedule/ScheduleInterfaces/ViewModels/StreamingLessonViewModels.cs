using Tools.ViewModels;

namespace ScheduleInterfaces.ViewModels
{
    public class StreamingLessonPageViewModel : PageSettingListViewModel<StreamingLessonViewModel> { }

	public class StreamingLessonViewModel : PageSettingElementViewModel
    {
        public string IncomingGroups { get; set; }

        public string StreamName { get; set; }
    }
}