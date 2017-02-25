using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий группу
    /// </summary>
    public class StudentGroup : BaseEntity
    {
        public long EducationDirectionId { get; set; }

        [MaxLength(20)]
        public string GroupName { get; set; }

        [Required]
        public int Kurs { get; set; }

        /// <summary>
        /// Предполагаемое количество студентов в группе (для первого курса)
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Количество подгрупп
        /// </summary>
        public int SubgroupsCount { get; set; }

        public long? StewardId { get; set; }

        public long? CuratorId { get; set; }

        public virtual EducationDirection EducationDirection { get; set; }

        [ForeignKey("StudentGroupId")]
        public virtual List<Student> Students { get; set; }

        /// <summary>
        /// Куратор группы
        /// </summary>
        public Lecturer Curator { get; set; }

        /// <summary>
        /// Староста группы
        /// </summary>
        public Student Steward { get; set; }
    }
}
