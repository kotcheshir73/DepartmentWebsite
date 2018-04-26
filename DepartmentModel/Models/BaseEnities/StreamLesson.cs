using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, хранящий информацию о потоках для учебных планов
    /// </summary>
    public class StreamLesson : BaseEntity
    {
        public Guid AcademicYearId { get; set; }

        public string StreamLessonName { get; set; }

        //-------------------------------------------------------------------------

        public AcademicYear AcademicYear { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("StreamLessonId")]
        public virtual List<StreamLessonRecord> StreamLessonRecords { get; set; }
    }
}
