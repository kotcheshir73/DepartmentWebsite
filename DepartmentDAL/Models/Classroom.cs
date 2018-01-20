using DepartmentDAL.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий аудиторию
    /// </summary>
    [DataContract]
    public class Classroom
    {
        [Key]
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public ClassroomTypes ClassroomType { get; set; }

        [DataMember]
        public int Capacity { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public DateTime? DateDelete { get; set; }
    }
}
