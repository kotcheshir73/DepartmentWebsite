using System;
using System.Collections.Generic;
using System.Text;
using Tools.ViewModels;

namespace WebInterfaces.ViewModels
{
    public class CommentPageViewModel : PageSettingListViewModel<CommentViewModel> { }
    public class CommentViewModel : PageSettingElementViewModel
    {
        public string Content { get; set; }

        public string DepartmentUser { get; set; }

        public DateTime Date { get; set; }       
    }
}
