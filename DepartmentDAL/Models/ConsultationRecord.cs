using System;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий разовую консультацию
    /// </summary>
    [DataContract]
    public class ConsultationRecord : ScheduleRecord
    {
        [DataMember]
        public DateTime DateConsultation { get; set; }
    }
}
