using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Преподавательская должность
    /// </summary>
    public class LecturerPost : BaseEntity
    {
        public string PostTitle { get; set; }

        public int Hours { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------

        [ForeignKey("LecturerPostId")]
        public virtual List<Lecturer> Lecturers { get; set; }
    }
}
