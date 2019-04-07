using System;

namespace ScheduleInterfaces.ViewModels
{
    public class ScheduleRecordShortViewModel
    {
        public Guid Id { get; set; }

        public string NotParseRecord { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }
    }

    public class ScheduleRecordViewModel
    {
        public Guid Id { get; set; }

        public string NotParseRecord { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }

        public Guid? ClassroomId { get; set; }

        public string Classroom { get; set; }

        public Guid? LecturerId { get; set; }

        public string Lecturer { get; set; }

        public Guid? DisciplineId { get; set; }

        public string Discipline { get; set; }

        public Guid? StudentGroupId { get; set; }

        public string StudentGroup { get; set; }
    }
}