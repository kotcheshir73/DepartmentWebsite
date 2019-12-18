using Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Schedule
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

        [DataMember]
        public string LessonConsultationClassroom { get; set; }

        //-------------------------------------------------------------------------

        public virtual Classroom ConsultationClassroom { get; set; }

        //-------------------------------------------------------------------------
    }
}