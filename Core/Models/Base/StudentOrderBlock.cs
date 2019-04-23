using Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Base
{
    /// <summary>
    /// Класс, описывающий блок приказа по студентам со своим типом приказа
    /// </summary>
    [DataContract]
    public class StudentOrderBlock : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid StudentOrderId { get; set; }
        
        [DataMember]
        public Guid? EducationDirectionId { get; set; }

        [Required]
        [DataMember]
        public StudentOrderType StudentOrderType { get; set; }

        //-------------------------------------------------------------------------

        public virtual StudentOrder StudentOrder { get; set; }

        public virtual EducationDirection EducationDirection { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("StudentOrderBlockId")]
        public virtual List<StudentOrderBlockStudent> StudentOrderBlockStudents { get; set; }
    }
}