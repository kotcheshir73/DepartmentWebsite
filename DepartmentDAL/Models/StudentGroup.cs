using DepartmentDAL.Enums;
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
        public AcademicCourse Course { get; set; }

        public string StewardId { get; set; }

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
		[ForeignKey("StewardId")]
		public Student Steward { get; set; }
    }
}
