using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    [DataContract]
    public class Message : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid UserId { get; set; }

        [MaxLength(150)]
        [Required]
        [DataMember]
        public string Caption { get; set; }
        
        [Required]
        [DataMember]
        public string Text { get; set; }

        //-------------------------------------------------------------------------

        public virtual User User { get; set; }

        //-------------------------------------------------------------------------
    }
}
