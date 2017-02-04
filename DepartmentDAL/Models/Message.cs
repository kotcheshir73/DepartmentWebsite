using System.ComponentModel.DataAnnotations;

namespace DepartmentDAL.Models
{
    public class Message : BaseEntity
    {
        public long UserId { get; set; }
        [Display(Name = "Заголовок")]
        [MaxLength(150)]
        [Required]
        public string Caption { get; set; }

        [Display(Name = "Текст сообщения")]
        [Required]
        public string Text { get; set; }

        public virtual User User { get; set; }
    }
}
