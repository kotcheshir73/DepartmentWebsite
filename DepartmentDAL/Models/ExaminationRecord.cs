using System;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий экзамен
    /// </summary>
    [DataContract]
    public class ExaminationRecord : ScheduleRecord
    {
        [DataMember]
        public DateTime DateConsultation { get; set; }

        [DataMember]
        public DateTime DateExamination { get; set; }

        [DataMember]
        public string LessonConsultationClassroom { get; set; }

        [DataMember]
        public string ConsultationClassroomId { get; set; }

        public virtual Classroom ConsultationClassroom { get; set; }
    }
}
