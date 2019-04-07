using System;
using Tools.ViewModels;

namespace ScheduleInterfaces.ViewModels
{
    public class ScheduleLessonTimePageViewModel : PageSettingListViewModel<ScheduleLessonTimeViewModel> { }

    public class ScheduleLessonTimeViewModel : PageSettingElementViewModel
    {
		public string Text { get; set; }

		public string Title { get; set; }

        public int Order { get; set; }

		public string TimeBeginLesson { get; set; }

		public string TimeEndLesson { get; set; }

		public DateTime DateBeginLesson { get; set; }

		public DateTime DateEndLesson { get; set; }
	}
}