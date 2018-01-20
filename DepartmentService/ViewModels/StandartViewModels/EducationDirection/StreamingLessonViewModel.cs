using System;

namespace DepartmentService.ViewModels
{
	public class StreamingLessonPageViewModel : PageViewModel<StreamingLessonViewModel> { }

	public class StreamingLessonViewModel
    {
        public Guid Id { get; set; }

        public string IncomingGroups { get; set; }

        public string StreamName { get; set; }
    }
}
