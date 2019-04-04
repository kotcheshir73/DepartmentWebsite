using Microsoft.AspNetCore.Identity;
using Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Authentication
{
    /// <summary>
    /// Класс, описывающий пользователя системы
    /// </summary>
    [DataContract]
    public class DepartmentUser : IdentityUser<Guid>
    {
        [DataMember]
        public DateTime DateCreate { get; set; }

        [DataMember]
        public DateTime? DateDelete { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public Guid? StudentId { get; set; }

        [DataMember]
        public Guid? LecturerId { get; set; }
        
        [DataMember]
        public byte[] Avatar { get; set; }
        
        [DataMember]
        public DateTime? DateLastVisit { get; set; }
        
        [DataMember]
        public DateTime? DateBanned { get; set; }

        //-------------------------------------------------------------------------

        public virtual Base.Lecturer Lecturer { get; set; }

        public virtual Student Student { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("UserId")]
        public virtual List<DepartmentUserRole> UserRoles { get; set; }

        public DepartmentUser()
        {
            Id = Guid.NewGuid();
            DateCreate = DateTime.Now;
            DateDelete = null;
            IsDeleted = false;
        }

        public DepartmentUser(string name)
        {
            Id = Guid.NewGuid();
            DateCreate = DateTime.Now;
            UserName = name;
            DateDelete = null;
            IsDeleted = false;
        }
    }
}