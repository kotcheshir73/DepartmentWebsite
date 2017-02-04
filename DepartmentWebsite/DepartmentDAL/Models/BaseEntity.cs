using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Базовый набор для сущности
    /// </summary>
    public class BaseEntity
    {
        public long Id { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Дата создания")]
        public DateTime DateCreate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Дата удаления")]
        public DateTime? DateDelete { get; set; }

        [Display(Name = "Удален")]
        public bool IsDeleted { get; set; }
    }
}
