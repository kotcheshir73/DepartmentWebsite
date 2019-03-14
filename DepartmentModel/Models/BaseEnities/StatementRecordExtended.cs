using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, хранящий отметку студента из ведомости по предмету
    /// </summary>
    [DataContract]
    public class StatementRecordExtended : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid StatementRecordId { get; set; }

        [DataMember]
        public string Name { get; set; }

        //-------------------------------------------------------------------------

        public virtual StatementRecord StatementRecord { get; set; }

        //-------------------------------------------------------------------------
    }
}
