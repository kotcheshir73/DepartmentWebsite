using System;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий разовую консультацию
    /// </summary>
    public class ConsultationRecord
    {
        public long Id { get; set; }

        public long SeasonDatesId { get; set; }

        public DateTime DateConsultation { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }

        public string ClassroomId { get; set; }

        public long? StudentGroupId { get; set; }

        public long? LecturerId { get; set; }

        public virtual SeasonDates SeasonDates { get; set; }

        public virtual Classroom Classroom { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }

        public virtual Lecturer Lecturer { get; set; }
    }
}
