using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Связка пользователь - роль
    /// </summary>
    [DataContract]
    public class UserRole
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public long RoleId { get; set; }

        [DataMember]
        public long UserId { get; set; }

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }
    }
}
