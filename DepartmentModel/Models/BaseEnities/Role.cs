using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, описывающий роль в системе
    /// </summary>
    [DataContract]
    public class Role : BaseEntity
    {
        [MaxLength(20)]
        [Required]
        [DataMember]
        public string RoleName { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------

        [ForeignKey("RoleId")]
        public virtual List<Access> Access { get; set; }

        [ForeignKey("RoleId")]
        public virtual List<UserRole> UserRoles { get; set; }
    }
}
