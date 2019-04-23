using System;
using System.ComponentModel.DataAnnotations;

namespace ScheduleInterfaces.BindingModels
{
    /// <summary>
    /// Получение расписания для аудитории, преподавателя или группы
    /// </summary>
    public class ScheduleGetBindingModel
    {
        public Guid? Id;

        public string ClassroomNumber { get; set; }

        public Guid? ClassroomId { get; set; }

        public string StudentGroupName { get; set; }

        public Guid? StudentGroupId { get; set; }

        public string DisciplineName { get; set; }

        public Guid? DisciplineId { get; set; }

        public string LecturerName { get; set; }

        public Guid? LecturerId { get; set; }

        public DateTime? DateBegin { get; set; }

        public DateTime? DateEnd { get; set; }

        public Guid? SeasonDateId { get; set; }

        public bool? IsFirstHalfSemester { get; set; }
    }

    /// <summary>
    /// Запись расписания для сохранения
    /// </summary>
    public class ScheduleSetBindingModel
    {
        public Guid Id;

        public Guid? SeasonDatesId { get; set; }

        public string NotParseRecord { get; set; }

        [Required(ErrorMessage = "required")]
        public string LessonDiscipline { get; set; }

        [Required(ErrorMessage = "required")]
        public string LessonLecturer { get; set; }

        [Required(ErrorMessage = "required")]
        public string LessonGroup { get; set; }

        [Required(ErrorMessage = "required")]
        public string LessonClassroom { get; set; }

        public Guid? LecturerId { get; set; }

        public Guid? StudentGroupId { get; set; }

        public Guid? ClassroomId { get; set; }

        public Guid? DisciplineId { get; set; }
    }
}