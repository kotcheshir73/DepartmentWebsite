using DepartmentModel.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, описывающий возможные действия для роли
    /// </summary>
    [DataContract]
    public class Access : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid RoleId { get; set; }
        
        [Required]
        [DataMember]
        public AccessOperation Operation { get; set; }

        [Required]
        [DataMember]
        public AccessType AccessType { get; set; }

        //-------------------------------------------------------------------------

        public virtual Role Role { get; set; }

        //-------------------------------------------------------------------------
    }
}
