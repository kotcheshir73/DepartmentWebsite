using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DepartmentWebCore.Models
{
    public class NewsWithFilesModel
    {
        public Guid Id { get; set; }

        public Guid DepartmentUserId { get; set; }

        public int CurrentPage { get; set; }

        [Display(Name = "Заголовок новости:")]
        public string Title { get; set; }

        [Display(Name = "Текст новости:")]
        public string Body { get; set; }

        [Display(Name = "Теги:")]
        public string Tag { get; set; }

        public string Author { get; set; }

        public DateTime Date { get; set; }

        public List<IFormFile> FilesForUpload { get; set; }

        public List<string> FilesForDownload { get; set; }
    }
}