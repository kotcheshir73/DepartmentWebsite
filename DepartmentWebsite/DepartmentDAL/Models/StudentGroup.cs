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

        public virtual EducationDirection EducationDirection { get; set; }

        [ForeignKey("StudentGroupId")]
        public virtual List<Student> Students { get; set; }
    }
}
