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
    public class Comment : BaseEntity
    {
        [DataMember]
        public string DepartmentUser { get; set; }

        [Required]
        [DataMember]
        public string Content { get; set; }
                
        [DataMember]
        public Guid? DisciplineId { get; set; }

        [DataMember]
        public Guid? EventId { get; set; }

        [DataMember]
        public Guid? ParentId { get; set; }

        //-------------------------------------------------------------------------
        
        public virtual Discipline Discipline { get; set; }

        public virtual Event Event { get; set; }

        //Вроде не нужно... Ссылки на родительские комментарии
        //public virtual Comment Parent { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("ParentId")]
        public virtual List<Comment> ChildComments { get; set; }

        //-------------------------------------------------------------------------
               
    }
}