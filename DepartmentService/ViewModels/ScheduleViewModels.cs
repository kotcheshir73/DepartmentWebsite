namespace DepartmentService.ViewModels
{
    public class SemesterRecordViewModel
    {
        public long Id { get; set; }

        public int Week { get; set; }

        public int Day { get; set; }

        public int Lesson { get; set; }

        public string LessonType { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonTeacher { get; set; }

        public string GroupName { get; set; }

        public string ClassroomNumber { get; set; }
    }
}
