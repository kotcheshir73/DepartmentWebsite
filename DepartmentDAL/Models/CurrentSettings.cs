using System.ComponentModel.DataAnnotations;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс для хранения различных текущих настроек системы
    /// </summary>
    public class CurrentSettings
    {
        [Key]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
