using Models.Authentication;
using Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Web
{
    [DataContract]
    public class Comment : BaseEntity
    {   
        [DataMember]
        public Guid DepartmentUserId { get; set; }
     
        [DataMember]
        public Guid? DisciplineId { get; set; }

        [DataMember]
        public Guid? NewsId { get; set; }

        [DataMember]
        public Guid? ParentId { get; set; }

        [Required]
        [DataMember]
        public string Content { get; set; }
        
        //-------------------------------------------------------------------------

        public virtual DepartmentUser DepartmentUser { get; set; }
        
        public virtual Discipline Discipline { get; set; }

        public virtual News News { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("ParentId")]
        public virtual List<Comment> Childs { get; set; }

        //-------------------------------------------------------------------------
               
    }
}