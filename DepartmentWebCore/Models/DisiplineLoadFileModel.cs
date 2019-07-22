using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace DepartmentWebCore.Models
{
    public class DisiplineLoadFileModel
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public List<IFormFile> FilesForUpload { get; set; }
    }
}