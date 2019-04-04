using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Authentication
{
    /// <summary>
    /// Класс, описывающий роль в системе
    /// </summary>
    [DataContract]
    public class DepartmentRole : IdentityRole<Guid>
    {
        [DataMember]
        public DateTime? DateDelete { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------

        [ForeignKey("RoleId")]
        public virtual List<DepartmentAccess> Access { get; set; }

        public DepartmentRole()
        {
            Id = Guid.NewGuid();
            DateDelete = null;
            IsDeleted = false;
        }

        public DepartmentRole(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            DateDelete = null;
            IsDeleted = false;
        }
    }
}