using DepartmentModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, описывающий пользователя системы
    /// </summary>
    [DataContract]
    public class User : BaseEntity
    {
        [DataMember]
        public Guid? StudentId { get; set; }

        [DataMember]
        public Guid? LecturerId { get; set; }

        [MaxLength(100)]
        [Required]
        [DataMember]
        public string Login { get; set; }
        
        [Required]
        [DataMember]
        public string Password { get; set; }
        
        [DataMember]
        public byte[] Avatar { get; set; }
        
        [DataMember]
        public DateTime? DateLastVisit { get; set; }
        
        [DataMember]
        public DateTime? DateBanned { get; set; }
        
        [DataMember]
        public bool IsBanned { get; set; }

        //-------------------------------------------------------------------------

        public virtual Lecturer Lecturer { get; set; }

        public virtual Student Student { get; set; }

        //-------------------------------------------------------------------------

        public virtual List<Message> Messages { get; set; }

        [ForeignKey("UserId")]
        public virtual List<UserRole> UserRoles { get; set; }
    }
}
