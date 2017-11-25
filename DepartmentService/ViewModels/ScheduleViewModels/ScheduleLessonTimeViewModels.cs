using System;

namespace DepartmentService.ViewModels
{
	public class ScheduleLessonTimeViewModel
	{
		public long Id { get; set; }

		public string Text { get; set; }

		public string Title { get; set; }

		public string TimeBeginLesson { get; set; }

		public string TimeEndLesson { get; set; }

		public DateTime DateBeginLesson { get; set; }

		public DateTime DateEndLesson { get; set; }
	}
}
