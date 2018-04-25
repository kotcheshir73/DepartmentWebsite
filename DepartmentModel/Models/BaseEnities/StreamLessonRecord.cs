using System;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, хранящий информацию о занятиях потока для учебных планов
    /// </summary>
    public class StreamLessonRecord : BaseEntity
    {
        public Guid StreamLessonId { get; set; }

        public Guid AcademicPlanRecordElementId { get; set; }

        public int Hours { get; set; }

        public bool IsMain { get; set; }

        //-------------------------------------------------------------------------

        public virtual StreamLesson StreamLesson { get; set; }

        public virtual AcademicPlanRecordElement AcademicPlanRecordElement { get; set; }

        //-------------------------------------------------------------------------
    }
}
