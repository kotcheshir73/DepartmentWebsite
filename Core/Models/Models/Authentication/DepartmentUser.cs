﻿using Models.Base;
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
    public class DepartmentUser : BaseEntity
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string PasswordHash { get; set; }

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

        public virtual Lecturer Lecturer { get; set; }

        public virtual Student Student { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("UserId")]
        public virtual List<DepartmentUserRole> UserRoles { get; set; }
    }
}