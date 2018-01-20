using DepartmentDAL.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий группу
    /// </summary>
    [DataContract]
    public class StudentGroup : BaseEntity
    {
        [DataMember]
        public long EducationDirectionId { get; set; }

        [MaxLength(20)]
        [DataMember]
        public string GroupName { get; set; }

        [Required]
        [DataMember]
        public AcademicCourse Course { get; set; }

        [DataMember]
        public long? CuratorId { get; set; }

        /// <summary>
        /// Староста группы ФИО
        /// </summary>
        [DataMember]        
        public string StewardName { get; set; }

        public virtual EducationDirection EducationDirection { get; set; }

        [ForeignKey("StudentGroupId")]
        public virtual List<Student> Students { get; set; }

        /// <summary>
        /// Куратор группы
        /// </summary>
        public virtual Lecturer Curator { get; set; }
    }
}
