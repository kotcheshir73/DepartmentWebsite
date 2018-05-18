using System;

namespace DepartmentService.ViewModels
{
    public class DisciplineBlockRecordPageViewModel : PageViewModel<DisciplineBlockRecordViewModel> { }

    public class DisciplineBlockRecordViewModel
    {
        public Guid Id { get; set; }

        public Guid DisciplineBlockId { get; set; }

        public Guid? EducationDirectionId { get; set; }

        public Guid AcademicYearId { get; set; }

        public Guid TimeNormId { get; set; }

        public string DisciplineBlockTitle { get; set; }

        public string EducationDirection { get; set; }

        public string AcademicYear { get; set; }

        public string TimeNormName { get; set; }

        public string DisciplineBlockRecordTitle { get; set; }

        public decimal DisciplineBlockRecordHours { get; set; }
    }
}
