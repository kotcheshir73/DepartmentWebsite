using DepartmentDAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий аудиторию
    /// </summary>
    public class Classroom
    {
        [Key]
        public string Id { get; set; }

        public ClassroomTypes ClassroomType { get; set; }

        public int Capacity { get; set; }
    }
}
