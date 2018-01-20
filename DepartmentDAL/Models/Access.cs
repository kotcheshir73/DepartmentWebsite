using DepartmentDAL.Enums;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий возможные действия для роли
    /// </summary>
    [DataContract]
    public class Access : BaseEntity
    {
        [DataMember]
        public long RoleId { get; set; }
        
        [Required]
        [DataMember]
        public AccessOperation Operation { get; set; }

        [DataMember]
        public AccessType AccessType { get; set; }

        public virtual Role Role { get; set; }
    }
}
