using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, хранящий контент к заданию
    /// На данный момент 2 варианта: текст или картинка
    /// </summary>
    [DataContract]
    public class DisciplineLessonTaskContext : BaseEntity
    {
        [DataMember]
        public int Order { get; set; }

        [DataMember]
        public long DisciplineLessonTaskId { get; set; }

		public virtual DisciplineLessonTask DisciplineLessonTask { get; set; }
	}
}
