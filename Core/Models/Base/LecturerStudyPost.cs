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
    public class LecturerStudyPost : BaseEntity
    {
        [DataMember]
        public string StudyPostTitle { get; set; }

        [DataMember]
        public int Hours { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------

        [ForeignKey("LecturerStudyPostId")]
        public virtual List<Lecturer> Lecturers { get; set; }

        //-------------------------------------------------------------------------

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            if (!string.IsNullOrEmpty(StudyPostTitle))
            {
                result.Append(StudyPostTitle);
            }
            return result.ToString();
        }
    }
}