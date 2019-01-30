using DepartmentModel.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ScheduleModels.Models
{
    /// <summary>
    /// Класс, описывающий занятие в семестре
    /// </summary>
    [DataContract]
    public class SemesterRecord : ScheduleRecord
    {
        [Required]
        [DataMember]
        public bool IsFirstHalfSemester { get; set; }

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

        /// <summary>
        /// Является ли пара подгрупповой
        /// </summary>
        [DataMember]
        public bool IsSubgroup { get; set; }

        /// <summary>
        /// При загрузке расписания отметка, проверена пара или нет
        /// </summary>
        [NotMapped]
        public bool Checked { get; set; }

        public SemesterRecord() : base()
        {
            Checked = false;
        }
    }
}