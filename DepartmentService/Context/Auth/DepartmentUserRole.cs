using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Runtime.Serialization;

namespace DepartmentService.Context
{
    /// <summary>
    /// Связка пользователь - роль
    /// </summary>
    [DataContract]
    public class DepartmentUserRole : IdentityUserRole<Guid>
    {
        //-------------------------------------------------------------------------

        public virtual DepartmentRole Role { get; set; }

        public virtual DepartmentUser User { get; set; }

        //-------------------------------------------------------------------------
    }
}
