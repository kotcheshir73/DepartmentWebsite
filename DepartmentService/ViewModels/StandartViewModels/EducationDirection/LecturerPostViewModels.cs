using System;

namespace DepartmentService.ViewModels
{
    public class LecturerPostPageViewModel : PageViewModel<LecturerPostViewModel> { }
    
    public class LecturerPostViewModel
    {
        public Guid Id { get; set; }

        public string PostTitle { get; set; }

        public int Hours { get; set; }
    }
}
