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

        public string LessonDiscipline { get; set; }

        public string LessonTeacher { get; set; }

        public long? StudentGroupId { get; set; }

        public string LessonGroupName { get; set; }

        public string ClassroomId { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }

        public virtual Classroom Classroom { get; set; }
    }
}
