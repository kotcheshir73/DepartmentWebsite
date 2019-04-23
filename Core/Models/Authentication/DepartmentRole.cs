using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Authentication
{
    /// <summary>
    /// Класс, описывающий роль в системе
    /// </summary>
    [DataContract]
    public class DepartmentRole : BaseEntity
    {
        [DataMember]
        public string RoleName { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------

        [ForeignKey("RoleId")]
        public virtual List<DepartmentAccess> Access { get; set; }
    }
}