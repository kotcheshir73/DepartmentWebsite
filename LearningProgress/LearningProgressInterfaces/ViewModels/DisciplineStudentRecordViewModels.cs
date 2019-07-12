using Enums;
using System;
using Tools.ViewModels;

namespace LearningProgressInterfaces.ViewModels
{
    public class DisciplineStudentRecordPageViewModel : PageSettingListViewModel<DisciplineStudentRecordViewModel> { }

    public class DisciplineStudentRecordViewModel : PageSettingElementViewModel
    {
        public Guid DisciplineId { get; set; }

        public Guid? StudentGroupId { get; set; }

        public Guid StudentId { get; set; }

        public string Discipline { get; set; }

        public string StudentGroup { get; set; }

        public Semesters Semester { get; set; }

        public string Student { get; set; }

        public string Variant { get; set; }
        
        public int SubGroup { get; set; }
    }
}