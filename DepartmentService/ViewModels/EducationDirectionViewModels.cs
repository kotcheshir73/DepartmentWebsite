using System.Collections.Generic;

namespace DepartmentService.ViewModels
{
	public class EducationDirectionPageViewModel
	{
		public int MaxCount { get; set; }

		public List<EducationDirectionViewModel> List { get; set; }
	}

	public class EducationDirectionViewModel
    {
        public long Id { get; set; }

        public string Cipher { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
    }
}
