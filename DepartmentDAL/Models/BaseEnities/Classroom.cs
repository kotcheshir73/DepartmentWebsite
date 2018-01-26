using DepartmentDAL.Enums;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий аудиторию
    /// </summary>
    [DataContract]
    public class Classroom : BaseEntity
    {
        [DataMember]
        public string Number { get; set; }

        [Required]
        [DataMember]
        public ClassroomTypes ClassroomType { get; set; }

        [Required]
        [DataMember]
        public int Capacity { get; set; }

        [Required]
        [DataMember]
        public bool NotUseInSchedule { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------
    }
}
