using System;

namespace DepartmentDAL.Models
{
    public class ExaminationRecord : ScheduleRecord
    {
        public DateTime DateConsultation { get; set; }

        public DateTime DateExamination { get; set; }
    }
}
