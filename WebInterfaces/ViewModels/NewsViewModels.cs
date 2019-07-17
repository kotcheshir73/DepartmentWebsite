using System;
using Tools.ViewModels;

namespace WebInterfaces.ViewModels
{
    public class NewsPageViewModel : PageSettingListViewModel<NewsViewModel>
    {
        public int CurrentPage { get; set; }
    }

    public class NewsViewModel : PageSettingElementViewModel
    {
        public Guid DepartmentUserId { get; set; }

        public string DepartmentUser { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set; }

        public string Tag { get; set; }
    }
}