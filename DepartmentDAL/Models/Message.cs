using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    [DataContract]
    public class Message : BaseEntity
    {
        [DataMember]
        public long UserId { get; set; }

        [MaxLength(150)]
        [Required]
        [DataMember]
        public string Caption { get; set; }
        
        [Required]
        [DataMember]
        public string Text { get; set; }

        public virtual User User { get; set; }
    }
}
