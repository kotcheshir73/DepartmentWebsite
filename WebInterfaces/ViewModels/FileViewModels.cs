using System;
using System.Collections.Generic;
using System.Text;
using Tools.ViewModels;

namespace WebInterfaces.ViewModels
{
    public class FilePageViewModel : PageSettingListViewModel<FileViewModel> { }
    public class FileViewModel : PageSettingElementViewModel
    {
        public string Name { get; set; }

        public string Path { get; set; }
    }
}
