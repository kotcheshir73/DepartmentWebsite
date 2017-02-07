using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий пользователя системы
    /// </summary>
    public class User : BaseEntity
    {
        public long RoleId { get; set; }

        public long? StudentId { get; set; }

        public long? LecturerId { get; set; }

        [Display(Name = "Логин (ФИО)")]
        [MaxLength(100)]
        [Required]
        public string Login { get; set; }

        [Display(Name = "Пароль (для студентов - номер зачетки)")]
        [MaxLength(20)]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Картинка")]
        [Required]
        public byte[] Avatar { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Дата последнего посещения")]
        public DateTime DateLastVisit { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Дата блокировки")]
        public DateTime? DateBanned { get; set; }

        [Display(Name = "Заблокирован")]
        public bool IsBanned { get; set; }

        public virtual Lecturer Lecturer { get; set; }

        public virtual Student Student { get; set; }

        public virtual Role Role { get; set; }

        public virtual List<Message> Messages { get; set; }
    }
}
