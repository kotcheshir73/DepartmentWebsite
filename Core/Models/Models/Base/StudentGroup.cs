using Models.Attributes;
using Models.Enums;
using Models.Examination;
using Models.LearningProgress;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Base
{
    /// <summary>
    /// Класс, описывающий группу
    /// </summary>
    [DataContract]
    [ClassUse("EducationDirection", "EducationDirectionId", "Группа привязана к направлению обучения")]
    [ClassUse("Lecturer", "CuratorId", "У группы может быть преподаватель-куратор")]
    public class StudentGroup : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid EducationDirectionId { get; set; }

        [DataMember]
        public Guid? CuratorId { get; set; }

        [MaxLength(20)]
        [Required]
        [DataMember]
        public string GroupName { get; set; }

        [Required]
        [DataMember]
        public AcademicCourse Course { get; set; }

        //-------------------------------------------------------------------------

        public virtual EducationDirection EducationDirection { get; set; }

        /// <summary>
        /// Куратор группы
        /// </summary>
        public virtual Lecturer Curator { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("StudentGroupId")]
        public virtual List<Student> Students { get; set; }

        [ForeignKey("StudentGroupId")]
        public virtual List<Statement> Statements { get; set; }

        [ForeignKey("StudentGroupId")]
        public virtual List<ExaminationList> ExaminationLists { get; set; }

        [ForeignKey("StudentGroupId")]
        public virtual List<DisciplineLessonConducted> DisciplineLessonConducteds { get; set; }
    }
}