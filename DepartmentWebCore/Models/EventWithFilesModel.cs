using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebInterfaces.ViewModels;

namespace DepartmentWebCore.Models
{
    public class EventWithFilesModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Tag { get; set; }

        public IFormFileCollection fileForUpload { get; set; }

        public List<WebProcessFileForDownloadViewModel> fileForDownload { get; set; }
    }
}
