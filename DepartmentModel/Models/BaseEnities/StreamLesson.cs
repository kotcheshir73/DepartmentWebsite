using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, хранящий информацию о потоках для учебных планов
    /// </summary>
    [DataContract]
    public class StreamLesson : BaseEntity
    {
        [DataMember]
        public Guid AcademicYearId { get; set; }

        [DataMember]
        public string StreamLessonName { get; set; }

        //-------------------------------------------------------------------------

        public AcademicYear AcademicYear { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("StreamLessonId")]
        public virtual List<StreamLessonRecord> StreamLessonRecords { get; set; }
    }
}
