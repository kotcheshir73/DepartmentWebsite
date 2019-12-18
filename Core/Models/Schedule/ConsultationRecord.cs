using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Schedule
{
    /// <summary>
    /// Класс, описывающий разовую консультацию
    /// </summary>
    [DataContract]
    public class ConsultationRecord : ScheduleRecord
    {
        [Required]
        [DataMember]
        public int ConsultationTime { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------
    }
}