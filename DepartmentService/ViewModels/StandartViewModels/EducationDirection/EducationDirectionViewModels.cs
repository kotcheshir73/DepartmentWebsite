using System;

namespace DepartmentService.ViewModels
{
	public class EducationDirectionPageViewModel : PageViewModel<EducationDirectionViewModel> { }

	public class EducationDirectionViewModel
    {
        public Guid Id { get; set; }

        public string Cipher { get; set; }

        public string ShortName { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }
    }
}
