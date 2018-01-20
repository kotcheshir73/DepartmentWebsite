using System;

namespace DepartmentService.ViewModels
{
	public class StudentGroupPageViewModel : PageViewModel<StudentGroupViewModel> { }

	public class StudentGroupViewModel
    {
        public Guid Id { get; set; }

        public Guid EducationDirectionId { get; set; }

        public Guid? CuratorId { get; set; }

        public string EducationDirectionCipher { get; set; }
        
        public string GroupName { get; set; }

        public int Course { get; set; }

        public int CountStudents { get; set; }

        public string StewardName { get; set; }

        public string Curator { get; set; }
	}
}
