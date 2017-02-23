using System;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий разовую консультацию
    /// </summary>
    public class ConsultationRecord : ScheduleRecord
    {
        public DateTime DateConsultation { get; set; }
    }
}
