using DepartmentDAL.Enums;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, хранящий информацию по сдаче студентом задания по занятию
    /// </summary>
    [DataContract]
    public class DisciplineLessonTaskStudentRecord : BaseEntity
    {
        [DataMember]
        public DisciplineLessonTaskStudentResult Result { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public long DisciplineLessonTaskId { get; set; }

        [DataMember]
        public string StudentId { get; set; }

		public virtual DisciplineLessonTask DisciplineLessonTask { get; set; }

		public virtual Student Student { get; set; }
	}
}
