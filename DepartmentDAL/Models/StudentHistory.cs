using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentDAL.Models
{

    /// <summary>
    /// Класс, хронящий историю по студенту
    /// </summary>
    public class StudentHistory
    {
        public long Id { get; set; }

        public string StudentId { get; set; }

        public DateTime DateCreate { get; set; }

        [MaxLength(150)]
        [Required]
        public string TextMessage { get; set; }

        public virtual Student Student { get; set; }
    }
}
