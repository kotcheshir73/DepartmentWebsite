using System;

namespace DepartmentService.ViewModels
{
    public class SoftwarePageViewModel : PageViewModel<SoftwareViewModel> { }

    public class SoftwareViewModel
    {
        public Guid Id { get; set; }

        public string SoftwareName { get; set; }

        public string SoftwareDescription { get; set; }

        public string SoftwareKey { get; set; }

        public string SoftwareK { get; set; }
    }
}
