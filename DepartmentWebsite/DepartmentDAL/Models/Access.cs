using System.ComponentModel.DataAnnotations;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий возможные действия для роли
    /// </summary>
    public class Access : BaseEntity
    {
        public long RoleId { get; set; }

        [Display(Name = "Название операции")]
        [MaxLength(100)]
        [Required]
        public string Operation { get; set; }

        public virtual Role Role { get; set; }
    }
}
