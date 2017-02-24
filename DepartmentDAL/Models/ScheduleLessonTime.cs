using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, который хранит даты начала и окончания пар/экзаменов
    /// </summary>
    public class ScheduleLessonTime
    {
        public long Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime DateBeginLesson { get; set; }

        [Required]
        public DateTime DateEndLesson { get; set; }
    }
}
