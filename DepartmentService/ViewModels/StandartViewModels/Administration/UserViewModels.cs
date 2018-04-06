using System;
using System.Drawing;

namespace DepartmentService.ViewModels
{
    public class UserPageViewModel : PageViewModel<UserViewModel> { }

    public class UserViewModel
    {
        public Guid Id { get; set; }

        public Guid? StudentId { get; set; }

        public Guid? LecturerId { get; set; }

        public string Login { get; set; }

        public Image Avatar { get; set; }

        public DateTime? DateLastVisit { get; set; }

        public DateTime? DateBanned { get; set; }

        public bool IsBanned { get; set; }
    }
}
