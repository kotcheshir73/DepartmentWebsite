using Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Examination
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
        public string Description { get; set; }

        [DataMember]
        public string Score { get; set; }

        //-------------------------------------------------------------------------

        public virtual Statement Statement { get; set; }

        public virtual Student Student { get; set; }

        //-------------------------------------------------------------------------
    }
}