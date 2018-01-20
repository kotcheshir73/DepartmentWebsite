using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, хранящий контент к заданию (картинка)
    /// </summary>
    [DataContract]
    public class DisciplineLessonTaskImageContext : DisciplineLessonTaskContext
    {
        [DataMember]
        public byte[] Image { get; set; }
	}
}
