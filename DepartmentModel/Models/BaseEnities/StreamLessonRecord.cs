using System;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, хранящий информацию о занятиях потока для учебных планов
    /// </summary>
    [DataContract]
    public class StreamLessonRecord : BaseEntity
    {
        [DataMember]
        public Guid StreamLessonId { get; set; }

        [DataMember]
        public Guid AcademicPlanRecordElementId { get; set; }

        [DataMember]
        public int Hours { get; set; }

        [DataMember]
        public bool IsMain { get; set; }

        //-------------------------------------------------------------------------

        public virtual StreamLesson StreamLesson { get; set; }

        public virtual AcademicPlanRecordElement AcademicPlanRecordElement { get; set; }

        //-------------------------------------------------------------------------
    }
}
