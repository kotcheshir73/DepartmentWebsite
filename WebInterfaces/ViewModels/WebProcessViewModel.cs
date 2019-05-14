using System;
using System.Collections.Generic;
using System.Text;
using Tools.ViewModels;

namespace WebInterfaces.ViewModels
{
    public class WebProcessLevelCommentPageViewModel : PageSettingListViewModel<WebProcessLevelCommentViewModel> { }
    public class WebProcessLevelCommentViewModel
    {
        public string Content { get; set; }

        public string DepartmentUser { get; set; }

        public DateTime Date { get; set; }

        public bool IsChild { get; set; }

        public Guid Id { get; set; }
    }
}
