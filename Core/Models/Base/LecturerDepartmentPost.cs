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
    public class LecturerDepartmentPost : BaseEntity
    {
        [DataMember]
        public string DepartmentPostTitle { get; set; }

        [DataMember]
        public int Order { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------

        [ForeignKey("LecturerDepartmentPostId")]
        public virtual List<Lecturer> Lecturers { get; set; }

        //-------------------------------------------------------------------------

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            if (!string.IsNullOrEmpty(DepartmentPostTitle))
            {
                result.Append(DepartmentPostTitle);
            }
            return result.ToString();
        }
    }
}