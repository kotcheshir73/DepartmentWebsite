using DepartmentModel.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DepartmentService.Context
{
    /// <summary>
    /// Класс, описывающий пользователя системы
    /// </summary>
    [DataContract]
    public class DepartmentUser : IdentityUser<Guid, DepartmentUserLogin, DepartmentUserRole, DepartmentUserClaim>
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

        public virtual Lecturer Lecturer { get; set; }

        public virtual Student Student { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("UserId")]
        public virtual List<DepartmentUserRole> UserRoles { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<DepartmentUser, Guid> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }

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
