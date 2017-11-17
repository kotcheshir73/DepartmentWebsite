using DepartmentDAL.Enums;
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
        [Required]
        public AccessOperation Operation { get; set; }

		public AccessType AccessType { get; set; }

        public virtual Role Role { get; set; }
    }
}
