using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models
{
    /// <summary>
    /// Тип Id сущности
    /// </summary>
    [DataContract]
    public class IdEntity
    {
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public IdEntity() : base()
        {
            Id = Guid.NewGuid();
        }
    }
}