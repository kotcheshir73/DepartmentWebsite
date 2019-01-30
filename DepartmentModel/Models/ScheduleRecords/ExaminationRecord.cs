using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
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

        /// <summary>
        /// При загрузке расписания отметка, проверена пара или нет
        /// </summary>
        [NotMapped]
        public bool Checked { get; set; }

        public ExaminationRecord() : base()
        {
            Checked = false;
        }

        //-------------------------------------------------------------------------

        public virtual Classroom ConsultationClassroom { get; set; }

        //-------------------------------------------------------------------------
    }
}
