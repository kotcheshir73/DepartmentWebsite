namespace DepartmentService.ViewModels
{
	public class EducationDirectionPageViewModel : PageViewModel<EducationDirectionViewModel> { }

	public class EducationDirectionViewModel
    {
        public long Id { get; set; }

        public string Cipher { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
    }
}
