using Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Authentication
{
    /// <summary>
    /// Класс, описывающий возможные действия для роли
    /// </summary>
    [DataContract]
    public class DepartmentAccess
    {
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [DataMember]
        public DateTime DateCreate { get; set; }

        [DataMember]
        public DateTime? DateDelete { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

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

        public DepartmentAccess()
        {
            Id = Guid.NewGuid();
            DateCreate = DateTime.Now;
            DateDelete = null;
            IsDeleted = false;
        }
    }
}