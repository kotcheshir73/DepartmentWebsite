using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Models.Base
{
    /// <summary>
    /// Класс, связывающий студента с приказом
    /// </summary>
    [DataContract]
    public class StudentOrderBlockStudent : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid StudentOrderBlockId { get; set; }

        [Required]
        [DataMember]
        public Guid StudentId { get; set; }
        
        [DataMember]
        public Guid? StudentGroupFromId { get; set; }
        
        [DataMember]
        public Guid? StudentGroupToId { get; set; }

        [DataMember]
        public string Info { get; set; }

        //-------------------------------------------------------------------------

        public virtual StudentOrderBlock StudentOrderBlock { get; set; }

        public virtual Student Student { get; set; }

        [NotMapped]
        public StudentGroup StudentGroupFrom { get; set; }

        [NotMapped]
        public StudentGroup StudentGroupTo { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(StudentOrderBlock?.ToString());
            result.Append(" ");
            result.Append(Student?.ToString());
            return result.ToString();
        }
    }
}