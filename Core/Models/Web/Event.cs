using Enums;
using Models.Attributes;
using Models.Authentication;
using Models.Examination;
using Models.LearningProgress;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Models.Web
{
    [DataContract]
    public class Event : BaseEntity
    {
        [Required]
        [DataMember]
        public string Title { get; set; }

        [Required]
        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public string DepartmentUser { get; set; }
        
        [DataMember]
        public string Tag { get; set; }        

        //-------------------------------------------------------------------------


        //-------------------------------------------------------------------------

        [ForeignKey("EventId")]
        public virtual List<Comment> Comments { get; set; }

        //-------------------------------------------------------------------------

    }
}