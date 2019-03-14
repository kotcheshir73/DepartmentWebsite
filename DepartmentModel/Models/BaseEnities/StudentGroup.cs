using DepartmentModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, описывающий группу
    /// </summary>
    [DataContract]
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

        /// <summary>
        /// Староста группы ФИО
        /// </summary>
        [DataMember]        
        public string StewardName { get; set; }

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
        public virtual List<Statement> Statement { get; set; }
        
        [ForeignKey("StudentGroupId")]
        public virtual List<DisciplineLessonConducted> DisciplineLessonConducteds { get; set; }
    }
}
