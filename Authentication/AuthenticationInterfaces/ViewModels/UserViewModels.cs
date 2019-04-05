using Interfaces.ViewModels;
using System;
using System.Drawing;

namespace AuthenticationInterfaces.ViewModels
{
    public class UserPageViewModel : PageSettingListViewModel<UserViewModel> { }

    public class UserViewModel : PageSettingElementViewModel
    {
        public Guid? StudentId { get; set; }

        public Guid? LecturerId { get; set; }

        public string Login { get; set; }

        public Image Avatar { get; set; }

        public DateTime? DateLastVisit { get; set; }

        public DateTime? DateBanned { get; set; }

        public bool IsBanned { get; set; }
    }
}