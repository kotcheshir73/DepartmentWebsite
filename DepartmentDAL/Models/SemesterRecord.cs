using DepartmentDAL.Enums;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий занятие в семестре
    /// </summary>
    [DataContract]
    public class SemesterRecord : ScheduleRecord
    {
        [DataMember]
        public int Week { get; set; }

        [DataMember]
        public int Day { get; set; }

        [DataMember]
        public int Lesson { get; set; }

        [DataMember]
        public LessonTypes LessonType { get; set; }

        /// <summary>
        /// Является ли пара потоковой
        /// </summary>
        [DataMember]
        public bool IsStreaming { get; set; }
    }
}
