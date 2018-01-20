using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, который хранит даты начала и окончания пар/экзаменов
    /// </summary>
    [DataContract]
    public class ScheduleLessonTime : BaseEntity
    {
        [MaxLength(100)]
        [Required]
        [DataMember]
        public string Title { get; set; }

        [Required]
        [DataMember]
        public int Order { get; set; }

        [Required]
        [DataMember]
        public DateTime DateBeginLesson { get; set; }

        [Required]
        [DataMember]
        public DateTime DateEndLesson { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------
    }
}
