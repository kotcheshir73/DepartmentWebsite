using DepartmentDAL.Enums;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий посещение занятия студентом
    /// </summary>
    [DataContract]
    public class DisciplineLessonStudentRecord : BaseEntity
    {
        [DataMember]
        public DisciplineLessonStudentStatus Status { get; set; }

        [DataMember]
        public long DisciplineLessonId { get; set; }

        [DataMember]
        public string StudentId { get; set; }

		public virtual DisciplineLesson DisciplineLesson { get; set; }

		public virtual Student Student { get; set; }
	}
}
