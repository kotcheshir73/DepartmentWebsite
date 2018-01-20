using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, хранящий контент к заданию (текст)
    /// </summary>
    [DataContract]
    public class DisciplineLessonTaskTextContext : DisciplineLessonTaskContext
    {
        [DataMember]
        public string Text { get; set; }
	}
}
