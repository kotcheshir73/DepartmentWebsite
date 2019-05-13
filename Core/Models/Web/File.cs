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
    public class File : BaseEntity
    {
        [Required]
        [DataMember]
        public string Name { get; set; }

        [Required]
        [DataMember]
        public string Path { get; set; }

        [Required]
        [DataMember]
        public string Format { get; set; }

        //-------------------------------------------------------------------------

        

        //-------------------------------------------------------------------------

        

        //-------------------------------------------------------------------------
               
    }
}