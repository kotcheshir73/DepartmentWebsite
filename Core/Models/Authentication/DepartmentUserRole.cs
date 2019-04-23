using System;
using System.Runtime.Serialization;

namespace Models.Authentication
{
    /// <summary>
    /// Связка пользователь - роль
    /// </summary>
    [DataContract]
    public class DepartmentUserRole : BaseEntity
    {
        [DataMember]
        public Guid RoleId { get; set; }

        [DataMember]
        public Guid UserId { get; set; }

        //-------------------------------------------------------------------------

        public virtual DepartmentRole Role { get; set; }

        public virtual DepartmentUser User { get; set; }

        //-------------------------------------------------------------------------
    }
}