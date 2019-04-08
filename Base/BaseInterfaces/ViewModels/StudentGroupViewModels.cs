using System;
using Tools.ViewModels;

namespace BaseInterfaces.ViewModels
{
	public class StudentGroupPageViewModel : PageSettingListViewModel<StudentGroupViewModel> { }

	public class StudentGroupViewModel : PageSettingElementViewModel
    {
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