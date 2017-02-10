namespace DepartmentService.ViewModels
{
    public class SemesterRecordViewModel
    {
        public long Id { get; set; }

        public int Week { get; set; }

        public int Day { get; set; }

        public int Lesson { get; set; }

        public string LessonType { get; set; }

        public bool IsStreaming { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }

        public string ClassroomId { get; set; }

        public string Classroom { get; set; }

        public long? LecturerId { get; set; }

        public string Lecturer { get; set; }

        public string Discipline { get; set; }

        public long? StudentGroupId { get; set; }

        public string StudentGroup { get; set; }
    }
}
