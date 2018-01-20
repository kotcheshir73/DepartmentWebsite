using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Связка пользователь - роль
    /// </summary>
    [DataContract]
    public class UserRole : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid RoleId { get; set; }

        [Required]
        [DataMember]
        public Guid UserId { get; set; }

        //-------------------------------------------------------------------------

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }

        //-------------------------------------------------------------------------
    }
}
