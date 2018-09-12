using DepartmentModel.Enums;
using System;

namespace DepartmentService.ViewModels
{
    public class DisciplineStudentRecordPageViewModel : PageViewModel<DisciplineStudentRecordViewModel> { }

    public class DisciplineStudentRecordViewModel
    {
        public Guid Id { get; set; }

        public Guid DisciplineId { get; set; }

        public Guid StudentId { get; set; }

        public string Discipline { get; set; }

        public string Student { get; set; }

        public Semesters Semester { get; set; }

        public string Variant { get; set; }
        
        public int SubGroup { get; set; }
    }
}
