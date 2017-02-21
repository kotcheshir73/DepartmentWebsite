using System;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий разовую консультацию
    /// </summary>
    public class ConsultationRecord : ScheduleRecord
    {
        /// <summary>
        /// Является ли пара потоковой
        /// </summary>
        public bool IsStreaming { get; set; }

        public DateTime DateConsultation { get; set; }
    }
}
