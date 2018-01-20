using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, хранящий историю по студенту
    /// </summary>
    [DataContract]
    public class StudentHistory : BaseEntity
    {
        [DataMember]
        public Guid StudentId { get; set; }

        [MaxLength(150)]
        [Required]
        [DataMember]
        public string TextMessage { get; set; }

        //-------------------------------------------------------------------------

        public virtual Student Student { get; set; }

        //-------------------------------------------------------------------------
    }
}
