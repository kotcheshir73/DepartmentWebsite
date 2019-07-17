using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DepartmentWebCore.Models
{
    public class NewsWithFilesModel
    {
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Текст")]
        public string Body { get; set; }

        [Display(Name = "Теги")]
        public string Tag { get; set; }

        public IFormFileCollection FilesForUpload { get; set; }
    }
}