using System;
using System.Runtime.Serialization;

namespace Models
{
    /// <summary>
    /// Базовый набор для сущности
    /// </summary>
    [DataContract]
    public class BaseEntity : IdEntity
    {
        [DataMember]
        public DateTime DateCreate { get; set; }

        [DataMember]
        public DateTime? DateDelete { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

        public BaseEntity() : base()
        {
            DateCreate = DateTime.Now;
            DateDelete = null;
            IsDeleted = false;
        }
    }
}