using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий зачет на зачетной неделе
    /// </summary>
    [DataContract]
    public class OffsetRecord : ScheduleRecord
    {
        [DataMember]
        public int Week { get; set; }

        [DataMember]
        public int Day { get; set; }

        [DataMember]
        public int Lesson { get; set; }
    }
}
