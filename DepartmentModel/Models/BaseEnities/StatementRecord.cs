using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, хранящий отметку студента из ведомости по предмету
    /// </summary>
    [DataContract]
    public class StatementRecord : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid StatementId { get; set; }

        [Required]
        [DataMember]
        public Guid StudentId { get; set; }

        [DataMember]
        public string Score { get; set; }

        //-------------------------------------------------------------------------

        public virtual Statement Statement { get; set; }

        public virtual Student Student { get; set; }

        //-------------------------------------------------------------------------
        
        [ForeignKey("StatementRecordId")]
        public virtual List<StatementRecordExtended> StatementRecordExtendeds { get; set; }
    }
}
