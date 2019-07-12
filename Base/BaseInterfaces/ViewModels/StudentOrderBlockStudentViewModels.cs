using System;
using Tools.ViewModels;

namespace BaseInterfaces.ViewModels
{
    public class StudentOrderBlockStudentPageViewModel : PageSettingListViewModel<StudentOrderBlockStudentViewModel> { }

    public class StudentOrderBlockStudentViewModel : PageSettingElementViewModel
    {
        public Guid StudentOrderBlockId { get; set; }

        public Guid StudentId { get; set; }

        public Guid? StudentGroupFromId { get; set; }

        public Guid? StudentGroupToId { get; set; }

        public string StudentOrderBlock { get; set; }

        public string Student { get; set; }

        public string StudentGromFrom { get; set; }

        public string StudentGroupTo { get; set; }

        public string Info { get; set; }
    }
}