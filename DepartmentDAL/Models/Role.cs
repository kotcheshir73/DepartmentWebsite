using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий роль в системе
    /// </summary>
    public class Role : BaseEntity
    {
        [Display(Name = "Название роли")]
        [MaxLength(20)]
        [Required]
        public string RoleName { get; set; }

        [ForeignKey("RoleId")]
        public virtual List<User> Users { get; set; }

        [ForeignKey("RoleId")]
        public virtual List<Access> Access { get; set; }
    }
}
