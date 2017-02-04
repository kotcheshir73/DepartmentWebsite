using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий преподавателя
    /// </summary>
    public class Lecturer : BaseEntity
    {
        [Display(Name = "Имя")]
        [MaxLength(20)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [MaxLength(30)]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        [MaxLength(30)]
        [Required]
        public string Patronymic { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Дата рождения")]
        [Required]
        public DateTime DateBirth { get; set; }

        [Display(Name = "Адрес")]
        [MaxLength(250)]
        [Required]
        public string Address { get; set; }

        [Display(Name = "Почта")]
        [MaxLength(150)]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Мобильный")]
        [MaxLength(50)]
        [Required]
        public string MobileNumber { get; set; }

        [Display(Name = "Домашний")]
        [MaxLength(50)]
        [Required]
        public string HomeNumber { get; set; }

        [Display(Name = "Должность")]
        [MaxLength(50)]
        [Required]
        public string Post { get; set; }

        [Display(Name = "Звание")]
        [MaxLength(50)]
        [Required]
        public string Rank { get; set; }

        [Display(Name = "О себе")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Фото")]
        [Required]
        public byte[] Photo { get; set; }
    }
}
