using System;
using System.Collections.Generic;
using Tools.ViewModels;

namespace WebInterfaces.ViewModels
{
    public class WebEducationDirectionPageViewModel : PageSettingListViewModel<WebEducationDirectionViewModel> { }

    public class WebEducationDirectionViewModel : PageSettingElementViewModel
    {
        public string Cipher { get; set; }

        public string ShortName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Qualification { get; set; }

        public List<Tuple<Guid, string>> Courses { get; set; }

        public override string ToString()
        {
            return $"{Cipher} {ShortName}";
        }
    }
}