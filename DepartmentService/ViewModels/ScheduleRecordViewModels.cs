namespace DepartmentService.ViewModels
{
    public class ScheduleRecordShortViewModels
    {
        public long Id { get; set; }

        public string NotParseRecord { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }
    }

    public class ScheduleRecordViewModels
    {
        public long Id { get; set; }

        public string NotParseRecord { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }

        public string ClassroomId { get; set; }

        public string Classroom { get; set; }

        public long? LecturerId { get; set; }

        public string Lecturer { get; set; }

        public long? DisciplineId { get; set; }

        public string Discipline { get; set; }

        public long? StudentGroupId { get; set; }

        public string StudentGroup { get; set; }
    }
}
