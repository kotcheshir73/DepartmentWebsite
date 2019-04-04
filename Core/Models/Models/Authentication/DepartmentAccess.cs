using Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Authentication
{
    /// <summary>
    /// Класс, описывающий возможные действия для роли
    /// </summary>
    [DataContract]
    public class DepartmentAccess : BaseEntity
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

        public virtual DepartmentRole Role { get; set; }

        //-------------------------------------------------------------------------
    }
}