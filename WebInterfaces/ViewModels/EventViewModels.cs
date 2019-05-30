using System;
using System.Collections.Generic;
using System.Text;
using Tools.ViewModels;

namespace WebInterfaces.ViewModels
{
    public class EventPageViewModel : PageSettingListViewModel<EventViewModel>
    {
        public int CurrentPage { get; set; }
    }
    public class EventViewModel : PageSettingElementViewModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string DepartmentUser { get; set; }

        public DateTime Date { get; set; }

        public string Tag { get; set; }         

    }
}
