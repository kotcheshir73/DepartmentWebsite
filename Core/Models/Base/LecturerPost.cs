using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Models.Base
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

        //-------------------------------------------------------------------------

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            if (!string.IsNullOrEmpty(PostTitle))
            {
                result.Append(PostTitle);
            }
            return result.ToString();
        }
    }
}