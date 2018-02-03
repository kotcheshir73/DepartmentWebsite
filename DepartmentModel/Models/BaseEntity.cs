using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Базовый набор для сущности
    /// </summary>
    [DataContract]
    public class BaseEntity
    {
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [DataMember]
        public DateTime DateCreate { get; set; }

        [DataMember]
        public DateTime? DateDelete { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

        public BaseEntity() : base()
        {
            Id = Guid.NewGuid();
            DateCreate = DateTime.Now;
            DateDelete = null;
            IsDeleted = false;
        }
    }
}
