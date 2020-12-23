using Models.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Web
{
    [DataContract]
    public class News : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid DepartmentUserId { get; set; }

        [Required]
        [DataMember]
        public string Title { get; set; }

        [Required]
        [DataMember]
        public string Body { get; set; }

        [DataMember]
        public string Tag { get; set; }

        //-------------------------------------------------------------------------

        public virtual DepartmentUser DepartmentUser { get; set; }


        //-------------------------------------------------------------------------

        [ForeignKey("NewsId")]
        public virtual List<Comment> Comments { get; set; }

        //-------------------------------------------------------------------------

    }
}