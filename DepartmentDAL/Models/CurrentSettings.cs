using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс для хранения различных текущих настроек системы
    /// </summary>
    [DataContract]
    public class CurrentSettings
    {
        [Key]
        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}
