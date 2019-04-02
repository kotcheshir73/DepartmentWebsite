using Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.LearningProgress
{
    /// <summary>
    /// Класс, описывающий проведение занятия
    /// </summary>
    [DataContract]
    public class DisciplineLessonConducted : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid DisciplineLessonId { get; set; }

        [Required]
        [DataMember]
        public Guid StudentGroupId { get; set; }

        [DataMember]
        public string Subgroup { get; set; }

        //-------------------------------------------------------------------------

        public virtual DisciplineLesson DisciplineLesson { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("DisciplineLessonConductedId")]
        public virtual List<DisciplineLessonConductedStudent> DisciplineLessonConductedStudents { get; set; }
    }
}