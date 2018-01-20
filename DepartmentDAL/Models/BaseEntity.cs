using System;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Базовый набор для сущности
    /// </summary>
    [DataContract]
    public class BaseEntity
    {
        [DataMember]
        public long Id { get; set; }
        
        [DataMember]
        public DateTime DateCreate { get; set; }
        
        [DataMember]
        public DateTime? DateDelete { get; set; }
        
        [DataMember]
        public bool IsDeleted { get; set; }
    }
}
