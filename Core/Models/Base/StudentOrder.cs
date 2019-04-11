using Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Base
{
    /// <summary>
    /// Класс, описывающий приказы по студентам
    /// </summary>
    [DataContract]
    public class StudentOrder : BaseEntity
    {
        [Required]
        [DataMember]
        public string OrderNumber { get; set; }

        [Required]
        [DataMember]
        public StudentOrderType StudentOrderType { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------

        [ForeignKey("StudentOrderId")]
        public virtual List<StudentOrderBlock> StudentOrderBlocks { get; set; }
    }
}