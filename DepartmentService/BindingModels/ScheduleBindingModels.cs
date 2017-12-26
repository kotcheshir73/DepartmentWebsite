using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	/// <summary>
	/// Получение расписания для аудитории, преподавателя или группы
	/// </summary>
	public class ScheduleGetBindingModel
    {
        public long? Id;

        public string ClassroomId { get; set; }

        public string StudentGroupName { get; set; }

        public long? StudentGroupId { get; set; }

        public string DisciplineName { get; set; }

        public long? DisciplineId { get; set; }

        public string LecturerName { get; set; }

        public long? LecturerId { get; set; }

        public DateTime? DateBegin { get; set; }

        public DateTime? DateEnd { get; set; }

        public long? SeasonDateId { get; set; }
    }

    /// <summary>
    /// Запись расписания для сохранения
    /// </summary>
    public class ScheduleRecordBindingModel
    {
        public long Id;

        public long? SeasonDatesId { get; set; }

        public string NotParseRecord { get; set; }

        [Required(ErrorMessage = "required")]
        public string LessonDiscipline { get; set; }

        [Required(ErrorMessage = "required")]
        public string LessonLecturer { get; set; }

        [Required(ErrorMessage = "required")]
        public string LessonGroup { get; set; }

        [Required(ErrorMessage = "required")]
        public string LessonClassroom { get; set; }

        public long? LecturerId { get; set; }

        public long? StudentGroupId { get; set; }

        public string ClassroomId { get; set; }

        public long? DisciplineId { get; set; }
    }
}
