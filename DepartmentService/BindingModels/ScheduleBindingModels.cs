using System;

namespace DepartmentService.BindingModels
{
	/// <summary>
	/// Получение расписания для аудитории, преподавателя или группы
	/// </summary>
	public class ScheduleBindingModel
    {
        public string ClassroomId { get; set; }

        public string GroupName { get; set; }

		public long? LecturerId { get; set; }

		public DateTime? DateBegin { get; set; }

        public DateTime? DateEnd { get; set; }
    }
}
