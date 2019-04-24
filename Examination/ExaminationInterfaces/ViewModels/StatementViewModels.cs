using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class StatementPageViewModel : PageSettingListViewModel<StatementViewModel> { }

    public class StatementViewModel : PageSettingElementViewModel
    {
        public Guid LecturerId { get; set; }

        public Guid DisciplineId { get; set; }

        public Guid AcademicYearId { get; set; }

        public Guid StudentGroupId { get; set; }

        public string LectureName { get; set; }

        public string DisciplineName { get; set; }

        public string StudentGroupName { get; set; }

        public string AcademicYearName { get; set; }

        public string TypeOfTest { get; set; }

        public string Semester { get; set; }

        public string Date { get; set; }

        public bool IsMain { get; set; }
    }
}