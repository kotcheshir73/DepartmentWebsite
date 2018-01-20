using System;
using System.ComponentModel.DataAnnotations;
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
        public Guid? ConsultationClassroomId { get; set; }

        [Required]
        [DataMember]
        public DateTime DateConsultation { get; set; }

        [Required]
        [DataMember]
        public DateTime DateExamination { get; set; }

        [DataMember]
        public string LessonConsultationClassroom { get; set; }

        //-------------------------------------------------------------------------

        public virtual Classroom ConsultationClassroom { get; set; }

        //-------------------------------------------------------------------------
    }
}
