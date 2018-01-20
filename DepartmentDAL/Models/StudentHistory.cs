using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, хронящий историю по студенту
    /// </summary>
    [DataContract]
    public class StudentHistory
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string StudentId { get; set; }

        [DataMember]
        public DateTime DateCreate { get; set; }

        [MaxLength(150)]
        [Required]
        [DataMember]
        public string TextMessage { get; set; }

        public virtual Student Student { get; set; }
    }
}
