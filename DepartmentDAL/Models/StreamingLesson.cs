using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, хранящий в себе сведенья о потоковых занятиях
    /// </summary>
    [DataContract]
    public class StreamingLesson : BaseEntity
    {
        [DataMember]
        public string IncomingGroups { get; set; }

        [DataMember]
        public string StreamName { get; set; }
    }
}
