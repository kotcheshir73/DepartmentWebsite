using System;
using Tools.ViewModels;

namespace WebInterfaces.ViewModels
{
    public class CommentPageViewModel : PageSettingListViewModel<CommentViewModel>
    {
        public Guid? DisciplineId { get; set; }

        public Guid? NewsId { get; set; }
    }

    public class CommentViewModel : PageSettingElementViewModel
    {
        public Guid DepartmentUserId { get; set; }

        public Guid? DisciplineId { get; set; }

        public Guid? NewsId { get; set; }

        public Guid? ParentId { get; set; }

        public string DepartmentUser { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }
        
        public int CountChilds { get; set; }
    }
}