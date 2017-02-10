using DepartmentDAL.Enums;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий занятие в семестре
    /// </summary>
    public class SemesterRecord
    {
        public long Id { get; set; }

        public int Week { get; set; }

        public int Day { get; set; }

        public int Lesson { get; set; }

        public LessonTypes LessonType { get; set; }

        /// <summary>
        /// Является ли пара потоковой
        /// </summary>
        public bool IsStreaming { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }

        public string ClassroomId { get; set; }

        public long? StudentGroupId { get; set; }

        public long? LecturerId { get; set; }

        public virtual Classroom Classroom { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }

        public virtual Lecturer Lecturer { get; set; }
    }
}
