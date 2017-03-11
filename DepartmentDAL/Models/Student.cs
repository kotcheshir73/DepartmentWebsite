using System.ComponentModel.DataAnnotations;

namespace DepartmentDAL.Models
{
    public class Student : BaseEntity
    {
        public long StudentGroupId { get; set; }

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
        public string Patronymic { get; set; }

        [Display(Name = "О себе")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Фото")]
        [Required]
        public byte[] Photo { get; set; }
        
        public virtual StudentGroup StudentGroup { get; set; }
    }
}
