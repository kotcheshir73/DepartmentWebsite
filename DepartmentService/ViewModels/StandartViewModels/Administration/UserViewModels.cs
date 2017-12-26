using System;
using System.Drawing;

namespace DepartmentService.ViewModels
{
    public class UserPageViewModel : PageViewModel<UserViewModel> { }

    public class UserViewModel
    {
        public long Id { get; set; }

        public long? StudentId { get; set; }

        public long? LecturerId { get; set; }

        public string Login { get; set; }

        public Image Avatar { get; set; }

        public DateTime? DateLastVisit { get; set; }

        public DateTime? DateBanned { get; set; }

        public string RoleType { get; set; }

        public bool IsBanned { get; set; }
    }
}
