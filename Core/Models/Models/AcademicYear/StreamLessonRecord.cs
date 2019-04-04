using System;
using System.Runtime.Serialization;

namespace Models.AcademicYear
{
    /// <summary>
    /// Класс, хранящий информацию о занятиях потока для учебных планов
    /// </summary>
    [DataContract]
    public class StreamLessonRecord : BaseEntity
    {
        [DataMember]
        public Guid StreamLessonId { get; set; }
        //TODO cicle
        [DataMember]
        public Guid AcademicPlanRecordElementId { get; set; }

        [DataMember]
        public bool IsMain { get; set; }

        //-------------------------------------------------------------------------

        public virtual StreamLesson StreamLesson { get; set; }

        public virtual AcademicPlanRecordElement AcademicPlanRecordElement { get; set; }

        //-------------------------------------------------------------------------
    }
}