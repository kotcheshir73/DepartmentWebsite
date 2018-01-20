using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий зачет на зачетной неделе
    /// </summary>
    [DataContract]
    public class OffsetRecord : ScheduleRecord
    {
        [Required]
        [DataMember]
        public int Week { get; set; }

        [Required]
        [DataMember]
        public int Day { get; set; }

        [Required]
        [DataMember]
        public int Lesson { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------
    }
}
