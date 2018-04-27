using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Преподавательская должность
    /// </summary>
    [DataContract]
    public class LecturerPost : BaseEntity
    {
        [DataMember]
        public string PostTitle { get; set; }

        [DataMember]
        public int Hours { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------

        [ForeignKey("LecturerPostId")]
        public virtual List<Lecturer> Lecturers { get; set; }
    }
}
