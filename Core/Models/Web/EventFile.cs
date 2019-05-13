using Enums;
using Models.Attributes;
using Models.Authentication;
using Models.Base;
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
    public class EventFile : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid EventId { get; set; }

        [Required]
        [DataMember]
        public Guid FileId { get; set; }       

        //-------------------------------------------------------------------------
        
        public virtual File File { get; set; }

        public virtual Event Event { get; set; }

        //-------------------------------------------------------------------------

        

        //-------------------------------------------------------------------------
               
    }
}