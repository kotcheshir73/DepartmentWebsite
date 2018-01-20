using DepartmentDAL.Enums;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий занятие в семестре
    /// </summary>
    [DataContract]
    public class SemesterRecord : ScheduleRecord
    {
        [Required]
        [DataMember]
        public int Week { get; set; }

        [Required]
        [DataMember]
        public int Day { get; set; }

        [Required]
        [DataMember]
        public int Lesson { get; set; }

        [Required]
        [DataMember]
        public LessonTypes LessonType { get; set; }

        /// <summary>
        /// Является ли пара потоковой
        /// </summary>
        [DataMember]
        public bool IsStreaming { get; set; }
    }
}
