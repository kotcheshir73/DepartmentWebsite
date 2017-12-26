using System;

namespace DepartmentDAL.Models
{
    public class ExaminationRecord : ScheduleRecord
    {
        public DateTime DateConsultation { get; set; }

        public DateTime DateExamination { get; set; }

        public string LessonConsultationClassroom { get; set; }

        public string ConsultationClassroomId { get; set; }

        public virtual Classroom ConsultationClassroom { get; set; }
    }
}
